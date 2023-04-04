using System.Text;
using MonBatSim.Core;

namespace MonBatSim.ConsoleApp;

internal static class Program
{
    private static readonly StackPrinter _stackPrinter = new();

    public static void Main()
    {
        _stackPrinter.Push("Monster Battle Simulator\n########################\n\n");
        var fighterName = QueryFighterName();
        _stackPrinter.Push($"Name: {fighterName}\n");
        var monsterRaceInputProcessor = new MonsterRaceInputProcessor();
        QueryForInput(monsterRaceInputProcessor);
        _stackPrinter.Push($"Race: {monsterRaceInputProcessor.MonsterRace}\n\n");
        var statDistribution = new StatDistribution(10f);
        var statMenuItem = StatMenuItem.HealthPoints;
        var currentInput = new StringBuilder();
        while (true)
        {
            _stackPrinter.Print();
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

    private static string QueryFighterName()
    { // The name ProcessFighterName is a work in process, because QueryFighterName is already taken at this point.
        while (true)
        {
            var fighterNameInputProcessor = new FighterNameInputProcessor();
            QueryForInput(fighterNameInputProcessor);
            var confirmationInputProcessor = new ConfirmationInputProcessor($"Is \"{fighterNameInputProcessor.FighterName}\" fine (Y/N)?: ");
            QueryForInput(confirmationInputProcessor);
            if (confirmationInputProcessor.IsConfirmed)
                return fighterNameInputProcessor.FighterName;
        }
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

    private static void QueryForInput(IConsoleInputProcessor consoleInputProcessor)
    {
        _stackPrinter.Push(consoleInputProcessor.Label);
        while (consoleInputProcessor.IsInputValid())
        {
            _stackPrinter.Print();
            consoleInputProcessor.Process();
        }
        _stackPrinter.Pop();
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