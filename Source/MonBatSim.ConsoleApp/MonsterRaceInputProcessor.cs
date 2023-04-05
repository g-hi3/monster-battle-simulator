using MonBatSim.Core;

namespace MonBatSim.ConsoleApp;

public class MonsterRaceInputProcessor : IConsoleInputProcessor
{
    private const string BaseLabel = "Pick a race: ";
    public MonsterRace MonsterRace { get; private set; } = MonsterRace.None;

    public bool IsInputValid()
    {
        return MonsterRaces.NamedValues.Contains(MonsterRace);
    }

    public void Process()
    {
        (Console.CursorLeft, Console.CursorTop) = (BaseLabel.Length, Console.CursorTop - MonsterRaces.NamedValues.Count());
        var stringValue = Console.ReadLine();
        MonsterRace = int.TryParse(stringValue, out var intValue)
            ? (MonsterRace) intValue
            : MonsterRace.None;
    }
}