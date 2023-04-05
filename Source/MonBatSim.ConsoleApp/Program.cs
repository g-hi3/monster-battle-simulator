using MonBatSim.Core;

namespace MonBatSim.ConsoleApp;

internal static class Program
{
    private static readonly StackPrinter _stackPrinter = new();
    private static readonly Random _random = new();

    public static void Main()
    {
        _stackPrinter.Push("Monster Battle Simulator\n########################\n\n");
        var fighterA = BuildFighter();
        _stackPrinter.Push($"{fighterA.Name} is ready to fight!\n\n");
        var fighterB = BuildFighter();
        _stackPrinter.Pop();
        _stackPrinter.Print();

        if (fighterA.Race == fighterB.Race)
        {
            Console.WriteLine("Fighters will not fight, because they belong to the same race!");
            return;
        }

        Console.WriteLine($"{fighterA.Name} vs {fighterB.Name}");

        while (fighterA.IsAlive() && fighterB.IsAlive())
        {
            var hpBefore = (fighterA.HealthPoints, fighterB.HealthPoints);
            if (Math.Abs(fighterA.Speed - fighterB.Speed) <= 0.001f
                && _random.NextSingle() < 0.5f
                || fighterA.Speed > fighterB.Speed)
            {
                DoAttack(fighterA, fighterB);
                DoAttack(fighterB, fighterA);
            }
            else
            {
                DoAttack(fighterB, fighterA);
                DoAttack(fighterA, fighterB);
            }

            var hpAfter = (fighterA.HealthPoints, fighterB.HealthPoints);

            if (hpBefore != hpAfter) continue;
            Console.WriteLine("Fight will loop infinitely!");
            return;
        }
        
        Console.WriteLine();
        Console.WriteLine(fighterA.IsAlive()
            ? $"{fighterA.Name} defeated {fighterB.Name}"
            : $"{fighterB.Name} defeated {fighterA.Name}");
        Console.ReadKey();
    }

    private static void DoAttack(Monster first, Monster second)
    {
        Console.WriteLine($"{first.Name} attacks!");
        first.Attack(second);
        Console.WriteLine($"{second.Name}'s HP dropped to {second.HealthPoints}!");
    }

    private static Monster BuildFighter()
    {
        var fighterName = QueryFighterName();
        _stackPrinter.Push($"Name: {fighterName}\n");
        var monsterRaceText = "\nPick a race: " + MonsterRaces.NamedValues
            .OrderBy(monsterRace => monsterRace)
            .Aggregate("", (totalString, monsterRace) => $"{totalString}\n {(int) monsterRace}: {monsterRace}");
        var monsterRaceInputProcessor = new MonsterRaceInputProcessor();
        QueryForInput(monsterRaceInputProcessor, monsterRaceText);
        var statDistribution = new StatDistribution(10f);
        var statsInputProcessor = new StatsInputProcessor(statDistribution);
        QueryForInput(statsInputProcessor, $"Race: {monsterRaceInputProcessor.MonsterRace}\n\n");
        _stackPrinter.Pop();
        return Monster.Create(fighterName, monsterRaceInputProcessor.MonsterRace, statDistribution);
    }

    private static string QueryFighterName()
    {
        while (true)
        {
            var fighterNameInputProcessor = new FighterNameInputProcessor();
            QueryForInput(fighterNameInputProcessor, "Enter fighter name: ");
            var confirmationInputProcessor = new ConfirmationInputProcessor();
            QueryForInput(confirmationInputProcessor, $"Is \"{fighterNameInputProcessor.FighterName}\" fine (Y/N)?: ");
            if (confirmationInputProcessor.IsConfirmed)
                return fighterNameInputProcessor.FighterName;
        }
    }

    private static void QueryForInput(IConsoleInputProcessor consoleInputProcessor, string? text = default)
    {
        if (text != default)
            _stackPrinter.Push(text);
        
        while (!consoleInputProcessor.IsInputValid())
        {
            _stackPrinter.Print();
            consoleInputProcessor.Process();
        }
        
        _stackPrinter.Pop();
    }
}