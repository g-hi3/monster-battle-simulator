namespace MonBatSim.Core.Test;

[Category("Unit")]
public sealed class CappedMonsterBuilderTests
{
    [Test]
    public void Build_ThrowsInvalidOperationException_WhenPointsSumIsGreaterThanPointsCap()
    {
        var monsterBuilder = new CappedMonsterBuilder(20f);
        monsterBuilder.AssignRace(MonsterRace.Goblin);
        monsterBuilder.AssignAttackPoints(15f);
        monsterBuilder.AssignDefensePoints(6f);
        
        Assert.That(monsterBuilder.Build, Throws.InvalidOperationException);
    }

    [Test]
    public void Build_ReturnsMonsterWithAssignedStats_When_PointsSumIsEqualToPointsCap()
    {
        const int healthPoints = 5;
        const int attackPoints = 4;
        const int defensePoints = 7;
        const int speed = 4;
        var monsterBuilder = new CappedMonsterBuilder(20f);
        monsterBuilder.AssignRace(MonsterRace.Ork);
        monsterBuilder.AssignHealthPoints(healthPoints);
        monsterBuilder.AssignAttackPoints(attackPoints);
        monsterBuilder.AssignDefensePoints(defensePoints);
        monsterBuilder.AssignSpeed(speed);

        var monster = monsterBuilder.Build();
        
        Assert.That(monster.HealthPoints, Is.EqualTo(healthPoints));
        Assert.That(monster.AttackPoints, Is.EqualTo(attackPoints));
        Assert.That(monster.DefensePoints, Is.EqualTo(defensePoints));
        Assert.That(monster.Speed, Is.EqualTo(speed));
    }
}