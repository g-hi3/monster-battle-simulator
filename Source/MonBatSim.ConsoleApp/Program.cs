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
        PrintIntro();
        PrintLabelledFighterName(fighterName);
        PrintLabelledMonsterRace(monsterRace);
        var statDistribution = new StatDistribution(10f);
        var statType = StatType.None;
        PrintStatDistribution(statDistribution, StatType.None);
        if (statType == StatType.None)
            Console.CursorVisible = false;
        Console.ReadLine();
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

    private static void PrintStatDistribution(StatDistribution statDistribution, StatType statType)
    {
        var remaining = statDistribution.Max - statDistribution.Total;
        Console.Write($"Stat distribution ({remaining}): ");
        if (statType == StatType.None)
            (Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);
        Console.WriteLine("  OK  ");
        if (statType == StatType.None)
            (Console.BackgroundColor, Console.ForegroundColor) = (Console.ForegroundColor, Console.BackgroundColor);
        float? healthPointsValue = statType != StatType.HealthPoints ? statDistribution.HealthPoints : null;
        Console.WriteLine($"Health points (HP): {healthPointsValue}");
        float? attackPointsValue = statType != StatType.AttackPoints ? statDistribution.AttackPoints : null;
        Console.WriteLine($"Attack points (AP): {attackPointsValue}");
        float? defensePointsValue = statType != StatType.DefensePoints ? statDistribution.DefensePoints : null;
        Console.WriteLine($"Defense points (DP): {defensePointsValue}");
        float? speedPointsValue = statType != StatType.Speed ? statDistribution.Speed : null;
        Console.WriteLine($"Speed: {speedPointsValue}");
        var verticalCursorOffset = statType switch
        {
            StatType.HealthPoints => 4,
            StatType.AttackPoints => 3,
            StatType.DefensePoints => 2,
            StatType.Speed => 1,
            _ => 0
        };
        (Console.CursorLeft, Console.CursorTop) = (20, Console.CursorTop - verticalCursorOffset);
    }

    private sealed class StatDistribution
    {
        private float _healthPoints;
        private float _attackPoints;
        private float _defensePoints;
        private float _speed;

        public StatDistribution(float max)
        {
            Max = max;
        }
        
        public float Max { get; }

        public float HealthPoints
        {
            get => _healthPoints;
            set => _healthPoints = Math.Min(Math.Max(value, 0f), Max - AttackPoints - DefensePoints - Speed);
        }

        public float AttackPoints
        {
            get => _attackPoints;
            set => _attackPoints = Math.Min(Math.Max(value, 0f), Max - HealthPoints - DefensePoints - Speed);
        }

        public float DefensePoints
        {
            get => _defensePoints;
            set => _defensePoints = Math.Min(Math.Max(value, 0f), Max - HealthPoints - AttackPoints - Speed);
        }

        public float Speed
        {
            get => _speed;
            set => _speed = Math.Min(Math.Max(value, 0f), Max - HealthPoints - AttackPoints - DefensePoints);
        }

        public float Total => HealthPoints + AttackPoints + DefensePoints + Speed;
    }

    private enum StatType
    {
        None = default,
        HealthPoints,
        AttackPoints,
        DefensePoints,
        Speed
    }
}