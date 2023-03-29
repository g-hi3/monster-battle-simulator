namespace MonBatSim.Core;

public static class MonsterRaces
{
    public static IEnumerable<MonsterRace> NamedValues => new[]
    {
        MonsterRace.Ork,
        MonsterRace.Troll,
        MonsterRace.Goblin
    };
}