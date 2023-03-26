namespace MonBatSim.Core.Test;

[Category("Unit")]
public sealed class MonsterBuilderTests
{
    [Test]
    public void CreateSimple_ReturnsSimpleMonsterBuilder()
    {
        var monsterBuilder = MonsterBuilder.CreateSimple();
        
        Assert.That(monsterBuilder, Is.InstanceOf<SimpleMonsterBuilder>());
    }

    [Test]
    public void CreateWithCap_ReturnsCappedMonsterBuilder()
    {
        const float pointsCap = 15f;
        var monsterBuilder = MonsterBuilder.WithCap(pointsCap);
        
        Assert.That(monsterBuilder, Is.InstanceOf<CappedMonsterBuilder>());
    }
}