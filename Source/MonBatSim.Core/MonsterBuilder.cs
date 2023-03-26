namespace MonBatSim.Core;

public static class MonsterBuilder
{
    public static IMonsterBuilder CreateSimple() => new SimpleMonsterBuilder();

    public static IMonsterBuilder WithCap(float pointsCap) => new CappedMonsterBuilder(pointsCap);
}