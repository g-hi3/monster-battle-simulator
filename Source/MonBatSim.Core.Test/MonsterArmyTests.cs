namespace MonBatSim.Core.Test;

[Category("Unit")]
public sealed class MonsterArmyTests
{
    [Test]
    public void HealthPoints_ReturnsSumOfAllMonsters()
    {
        var monsterA = new Monster { HealthPoints = 22f };
        var monsterB = new Monster { HealthPoints = 16f };
        var monsterC = new Monster { HealthPoints = 10f };
        var monsterArmy = new MonsterArmy(new[] { monsterA, monsterB, monsterC });
        
        Assert.That(monsterArmy.HealthPoints, Is.EqualTo(48f));
    }

    [Test]
    public void AttackPoints_ReturnsHighestOfAllMonsters()
    {
        var monsterA = new Monster { AttackPoints = 7f };
        var monsterB = new Monster { AttackPoints = 21f };
        var monsterC = new Monster { AttackPoints = 18f };
        var monsterArmy = new MonsterArmy(new[] { monsterA, monsterB, monsterC });
        
        Assert.That(monsterArmy.AttackPoints, Is.EqualTo(21f));
    }

    [Test]
    public void DefensePoints_ReturnsHighestOfAllMonsters()
    {
        var monsterA = new Monster { DefensePoints = 32f };
        var monsterB = new Monster { DefensePoints = 18f };
        var monsterC = new Monster { DefensePoints = 11f };
        var monsterArmy = new MonsterArmy(new[] { monsterA, monsterB, monsterC });
        
        Assert.That(monsterArmy.DefensePoints, Is.EqualTo(32f));
    }

    [Test]
    public void Speed_ReturnsAverageOfAllMonsters()
    {
        var monsterA = new Monster { Speed = 34f };
        var monsterB = new Monster { Speed = 18f };
        var monsterC = new Monster { Speed = 11f };
        var monsterArmy = new MonsterArmy(new[] { monsterA, monsterB, monsterC });
        
        Assert.That(monsterArmy.Speed, Is.EqualTo(21f));
    }

    [Test]
    public void Attack_AttacksMonstersIndividually()
    {
        var monsterA1 = new Monster
        {
            HealthPoints = 1f,
            AttackPoints = 17f
        };
        var monsterA2 = new Monster
        {
            HealthPoints = 21f,
            AttackPoints = 14f
        };
        var monsterA3 = new Monster
        {
            HealthPoints = 16f,
            AttackPoints = 4f
        };
        var armyA = new MonsterArmy(new[] { monsterA1, monsterA2, monsterA3 });
        var monsterB1 = new Monster
        {
            HealthPoints = 23f,
            DefensePoints = 3f
        };
        var monsterB2 = new Monster
        {
            HealthPoints = 20f,
            DefensePoints = 5f
        };
        var armyB = new MonsterArmy(new[] { monsterB1, monsterB2 });

        armyA.Attack(armyB);
        
        Assert.Multiple(() =>
        {
            Assert.That(monsterB2.HealthPoints, Is.EqualTo(0f));
            Assert.That(monsterB1.HealthPoints, Is.EqualTo(22f));
        });
    }

    [Test]
    public void Attack_OnlyAttacksForMonstersThatAreAlive()
    {
        var monsterA1 = new Monster
        {
            HealthPoints = 20f,
            AttackPoints = 15f
        };
        var monsterA2 = new Monster
        {
            HealthPoints = 0f,
            AttackPoints = 12f
        };
        var armyA = new MonsterArmy(new[] { monsterA1, monsterA2 });
        var monsterB = new Monster
        {
            HealthPoints = 20f,
            DefensePoints = 2f
        };
        var armyB = new MonsterArmy(new[] { monsterB });
        
        armyA.Attack(armyB);
        
        Assert.That(monsterB.HealthPoints, Is.EqualTo(7f));
    }

    [Test]
    public void Attack_AttackOrderIsBySpeed()
    {
        var monsterA1 = new Monster
        {
            HealthPoints = 13f,
            AttackPoints = 10f,
            Speed = 10f
        };
        var monsterA2 = new Monster
        {
            HealthPoints = 2f,
            AttackPoints = 21f,
            Speed = 100f
        };
        var armyA = new MonsterArmy(new[] { monsterA1, monsterA2 });
        var monsterB1 = new Monster
        {
            HealthPoints = 5f,
            DefensePoints = 10f
        };
        var monsterB2 = new Monster
        {
            HealthPoints = 16f,
            DefensePoints = 5f
        };
        var armyB = new MonsterArmy(new[] { monsterB1, monsterB2 });

        armyA.Attack(armyB);
        
        Assert.Multiple(() =>
        {
            Assert.That(monsterB1.HealthPoints, Is.EqualTo(0f));
            Assert.That(monsterB2.HealthPoints, Is.EqualTo(11f));
        });
    }
}