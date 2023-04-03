using System.Text;
using MonBatSim.Core;

namespace MonBatSim.ConsoleApp;

internal static class Program
{
    public static void Main()
    {
        var fighterName = string.Empty;
        var confirmedFighterName = false;
        while (!confirmedFighterName)
        {
            PrintIntro();
            PrintFighterNameQuery();
            fighterName = QueryFighterName();
            Console.Clear();
            string? input = default;
            while (input is not "Y" and not "N")
            {
                PrintIntro();
                PrintFighterNameConfirmation(fighterName);
                input = Console.ReadLine()?.Trim().ToUpper();
                Console.Clear();
            }

            confirmedFighterName = input == "Y";
        }
        var monsterRace = MonsterRace.None;
        while (!MonsterRaces.NamedValues.Contains(monsterRace))
        {
            PrintIntro();
            PrintLabelledFighterName(fighterName);
            Console.WriteLine();
            PrintMonsterRaceQuery();
            monsterRace = QueryMonsterRace();
            Console.Clear();
        }
        var statDistribution = new StatDistribution(10f);
        var statMenuItem = StatMenuItem.HealthPoints;
        var currentInput = new StringBuilder();
        while (true)
        {
            Console.Clear();
            PrintIntro();
            PrintLabelledFighterName(fighterName);
            PrintLabelledMonsterRace(monsterRace);
            PrintStatDistribution(statDistribution, statMenuItem, $"{currentInput}");
            Console.CursorVisible = statMenuItem is not StatMenuItem.None and not StatMenuItem.Confirm;
            var inputKey = Console.ReadKey();
            if (inputKey.Key is ConsoleKey.UpArrow or ConsoleKey.W)
            {
                currentInput.Clear();
                statMenuItem = MoveUp(statMenuItem);
            }
            else if (inputKey.Key is ConsoleKey.DownArrow or ConsoleKey.S)
            {
                currentInput.Clear();
                statMenuItem = MoveDown(statMenuItem);
            }
            else if (inputKey.Key is ConsoleKey.Backspace)
            {
                currentInput.Remove(currentInput.Length - 1, 1);
            }
            else if (inputKey.Key is ConsoleKey.Enter)
            {
                if (statMenuItem == StatMenuItem.Confirm)
                    break;
                
                float? inputValue = float.TryParse($"{currentInput}", out var floatValue) ? floatValue : null;
                currentInput.Clear();
                UpdateStatInDistribution(inputValue, statMenuItem, statDistribution);

                statMenuItem = MoveDown(statMenuItem);
            }
            else
            {
                currentInput.Append(inputKey.KeyChar);
            }
        }
        
        Console.WriteLine("We're done here for now");
        Console.ReadKey();
    }

    private static void UpdateStatInDistribution(
        float? inputValue,
        StatMenuItem statMenuItem,
        StatDistribution statDistribution)
    {
        if (!inputValue.HasValue)
            return;

        switch (statMenuItem)
        {
            case StatMenuItem.HealthPoints:
                statDistribution.HealthPoints = inputValue.Value;
                break;
            case StatMenuItem.AttackPoints:
                statDistribution.AttackPoints = inputValue.Value;
                break;
            case StatMenuItem.DefensePoints:
                statDistribution.DefensePoints = inputValue.Value;
                break;
            case StatMenuItem.Speed:
                statDistribution.Speed = inputValue.Value;
                break;
            case StatMenuItem.None:
            case StatMenuItem.Confirm:
            default:
                break;
        }
    }

    private static StatMenuItem MoveUp(StatMenuItem item)
    {
        return item switch
        {
            StatMenuItem.None => StatMenuItem.Confirm,
            StatMenuItem.Confirm => StatMenuItem.Speed,
            StatMenuItem.HealthPoints => StatMenuItem.Confirm,
            StatMenuItem.AttackPoints => StatMenuItem.HealthPoints,
            StatMenuItem.DefensePoints => StatMenuItem.AttackPoints,
            StatMenuItem.Speed => StatMenuItem.DefensePoints,
            _ => default
        };
    }

    private static StatMenuItem MoveDown(StatMenuItem item)
    {
        return item switch
        {
            StatMenuItem.None => StatMenuItem.HealthPoints,
            StatMenuItem.Confirm => StatMenuItem.HealthPoints,
            StatMenuItem.HealthPoints => StatMenuItem.AttackPoints,
            StatMenuItem.AttackPoints => StatMenuItem.DefensePoints,
            StatMenuItem.DefensePoints => StatMenuItem.Speed,
            StatMenuItem.Speed => StatMenuItem.Confirm,
            _ => default
        };
    }

    private static void PrintIntro()
    {
        Console.WriteLine("Monster Battle Simulator");
        Console.WriteLine("########################");
        Console.WriteLine();
    }

    private static void PrintFighterNameQuery()
    {
        Console.Write("Enter fighter name: ");
    }

    private static void PrintFighterNameConfirmation(string fighterName)
    {
        Console.Write($"Is \"{fighterName}\" fine (Y/N)?: ");
    }

    private static void PrintLabelledFighterName(string fighterName)
    {
        Console.WriteLine($"Name: {fighterName}");
    }

    private static string QueryFighterName()
    {
        string? fighterName = default;
        while (string.IsNullOrWhiteSpace(fighterName))
            fighterName = Console.ReadLine();
        return fighterName;
    }

    private static void PrintMonsterRaceQuery()
    {
        Console.WriteLine("Pick a race: ");
        var orderedMonsterNames = MonsterRaces.NamedValues
            .OrderBy(monsterRace => monsterRace)
            .ToList();
        orderedMonsterNames.ForEach(monsterRace => Console.WriteLine($" {(int)monsterRace}: {monsterRace}"));
        (Console.CursorLeft, Console.CursorTop) = (13, Console.CursorTop - orderedMonsterNames.Count - 1);
    }

    private static MonsterRace QueryMonsterRace()
    {
        var stringValue = Console.ReadLine();
        return int.TryParse(stringValue, out var intValue)
            ? (MonsterRace) intValue
            : MonsterRace.None;
    }

    private static void PrintLabelledMonsterRace(MonsterRace monsterRace)
    {
        Console.WriteLine($"Race: {monsterRace}");
    }

    private static void PrintStatDistribution(
        StatDistribution statDistribution,
        StatMenuItem statMenuItem,
        string currentInput)
    {
        var remaining = statDistribution.Max - statDistribution.Total;
        Console.Write($"Stat distribution ({remaining}): ");
        if (statMenuItem == StatMenuItem.Confirm)
        {
            (Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);
            Console.Write("  OK  ");
            (Console.BackgroundColor, Console.ForegroundColor) = (Console.ForegroundColor, Console.BackgroundColor);
        }
        
        Console.WriteLine();

        var healthPointsValue = statMenuItem != StatMenuItem.HealthPoints
            ? $"{statDistribution.HealthPoints}"
            : currentInput;
        Console.WriteLine($"Health points (HP): {healthPointsValue}");
        var attackPointsValue = statMenuItem != StatMenuItem.AttackPoints
            ? $"{statDistribution.AttackPoints}"
            : currentInput;
        Console.WriteLine($"Attack points (AP): {attackPointsValue}");
        var defensePointsValue = statMenuItem != StatMenuItem.DefensePoints
            ? $"{statDistribution.DefensePoints}"
            : currentInput;
        Console.WriteLine($"Defense points (DP): {defensePointsValue}");
        var speedPointsValue = statMenuItem != StatMenuItem.Speed
            ? $"{statDistribution.Speed}"
            : currentInput;
        Console.WriteLine($"Speed: {speedPointsValue}");
        var horizontalCursorPosition = statMenuItem switch
        {
            StatMenuItem.DefensePoints => 21,
            StatMenuItem.Speed => 7,
            _ => 20
        } + currentInput.Length;
        var verticalCursorOffset = statMenuItem switch
        {
            StatMenuItem.HealthPoints => 4,
            StatMenuItem.AttackPoints => 3,
            StatMenuItem.DefensePoints => 2,
            StatMenuItem.Speed => 1,
            _ => 0
        };
        (Console.CursorLeft, Console.CursorTop) = (horizontalCursorPosition, Console.CursorTop - verticalCursorOffset);
    }

    private enum StatMenuItem
    {
        None = default,
        HealthPoints,
        AttackPoints,
        DefensePoints,
        Speed,
        Confirm
    }
}