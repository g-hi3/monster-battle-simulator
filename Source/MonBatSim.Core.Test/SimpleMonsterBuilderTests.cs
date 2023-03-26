namespace MonBatSim.Core.Test;

[Category("Unit")]
public sealed class SimpleMonsterBuilderTests
{
    [DatapointSource] public readonly MonsterRace[] Races = { MonsterRace.Ork, MonsterRace.Goblin, MonsterRace.Troll };
    
    [Theory]
    public void Build_ReturnsMonsterWithAssignedRace(MonsterRace race)
    {
        var monsterBuilder = new SimpleMonsterBuilder();
        monsterBuilder.AssignRace(race);

        var monster = monsterBuilder.Build();
        
        Assert.That(monster.Race, Is.EqualTo(race));
    }

    [Test]
    public void Build_ThrowsInvalidOperationException_When_AssignedRaceIsNone()
    {
        var monsterBuilder = new SimpleMonsterBuilder();
        monsterBuilder.AssignRace(MonsterRace.None);
        
        Assert.That(monsterBuilder.Build, Throws.InvalidOperationException);
    }

    [Test]
    public void Build_ReturnsMonsterWithAssignedHealthPoints()
    {
        const float healthPoints = 20f;
        var monsterBuilder = new SimpleMonsterBuilder();
        monsterBuilder.AssignRace(MonsterRace.Goblin);
        monsterBuilder.AssignHealthPoints(healthPoints);

        var monster = monsterBuilder.Build();
        
        Assert.That(monster.HealthPoints, Is.EqualTo(healthPoints));
    }

    [Test]
    public void Build_ReturnsMonsterWithZeroHealthPoints_When_GivenValueIsNegative()
    {
        var monsterBuilder = new SimpleMonsterBuilder();
        monsterBuilder.AssignRace(MonsterRace.Goblin);
        monsterBuilder.AssignHealthPoints(-22f);

        var monster = monsterBuilder.Build();
        
        Assert.That(monster.HealthPoints, Is.Zero);
    }

    [Test]
    public void Build_ReturnsMonsterWithAssignedAttackPoints()
    {
        const float attackPoints = 14f;
        var monsterBuilder = new SimpleMonsterBuilder();
        monsterBuilder.AssignRace(MonsterRace.Goblin);
        monsterBuilder.AssignAttackPoints(attackPoints);

        var monster = monsterBuilder.Build();
        
        Assert.That(monster.AttackPoints, Is.EqualTo(attackPoints));
    }

    [Test]
    public void Build_ReturnsMonsterWithZeroAttackPoints_When_GivenValueIsNegative()
    {
        var monsterBuilder = new SimpleMonsterBuilder();
        monsterBuilder.AssignRace(MonsterRace.Goblin);
        monsterBuilder.AssignAttackPoints(-22);

        var monster = monsterBuilder.Build();
        
        Assert.That(monster.AttackPoints, Is.Zero);
    }

    [Test]
    public void Build_ReturnsMonsterWithAssignedDefensePoints()
    {
        const float defensePoints = 16f;
        var monsterBuilder = new SimpleMonsterBuilder();
        monsterBuilder.AssignRace(MonsterRace.Goblin);
        monsterBuilder.AssignDefensePoints(defensePoints);

        var monster = monsterBuilder.Build();
        
        Assert.That(monster.DefensePoints, Is.EqualTo(defensePoints));
    }

    [Test]
    public void Build_ReturnsMonsterWithZeroDefensePoints_When_GivenValueIsNegative()
    {
        var monsterBuilder = new SimpleMonsterBuilder();
        monsterBuilder.AssignRace(MonsterRace.Goblin);
        monsterBuilder.AssignDefensePoints(-6f);

        var monster = monsterBuilder.Build();
        
        Assert.That(monster.DefensePoints, Is.Zero);
    }

    [Test]
    public void Build_ReturnsMonsterWithAssignedSpeed()
    {
        const float speed = 17f;
        var monsterBuilder = new SimpleMonsterBuilder();
        monsterBuilder.AssignRace(MonsterRace.Goblin);
        monsterBuilder.AssignSpeed(speed);

        var monster = monsterBuilder.Build();
        
        Assert.That(monster.Speed, Is.EqualTo(speed));
    }

    [Test]
    public void Build_ReturnsMonsterWithZeroSpeed_When_GivenValueIsNegative()
    {
        var monsterBuilder = new SimpleMonsterBuilder();
        monsterBuilder.AssignRace(MonsterRace.Goblin);
        monsterBuilder.AssignSpeed(-47f);

        var monster = monsterBuilder.Build();
        
        Assert.That(monster.Speed, Is.Zero);
    }
}