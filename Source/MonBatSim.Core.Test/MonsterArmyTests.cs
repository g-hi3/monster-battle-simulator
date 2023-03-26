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
    public void AttackPoints_ReturnsSumOfAllMonsters()
    {
        var monsterA = new Monster { AttackPoints = 7f };
        var monsterB = new Monster { AttackPoints = 21f };
        var monsterC = new Monster { AttackPoints = 18f };
        var monsterArmy = new MonsterArmy(new[] { monsterA, monsterB, monsterC });
        
        Assert.That(monsterArmy.AttackPoints, Is.EqualTo(46f));
    }

    [Test]
    public void DefensePoints_ReturnsSumOfAllMonsters()
    {
        var monsterA = new Monster { DefensePoints = 32f };
        var monsterB = new Monster { DefensePoints = 18f };
        var monsterC = new Monster { DefensePoints = 11f };
        var monsterArmy = new MonsterArmy(new[] { monsterA, monsterB, monsterC });
        
        Assert.That(monsterArmy.DefensePoints, Is.EqualTo(61f));
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
}