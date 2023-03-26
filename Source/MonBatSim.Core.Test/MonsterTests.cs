namespace MonBatSim.Core.Test;

[Category("Unit")]
public sealed class MonsterTests
{
    [Test]
    public void Attack_ReducesHealthPoints()
    {
        var monsterA = new Monster { AttackPoints = 12f };
        var monsterB = new Monster
        {
            HealthPoints = 20f,
            DefensePoints = 8f
        };

        monsterA.Attack(monsterB);
        
        Assert.That(monsterB.HealthPoints, Is.EqualTo(16f));
    }

    [Test]
    public void Attack_DoesNotReduceHealthPoints_When_AttackIsLowerThanDefense()
    {
        var monsterA = new Monster { AttackPoints = 8f };
        var monsterB = new Monster
        {
            HealthPoints = 20f,
            DefensePoints = 12f
        };

        monsterA.Attack(monsterB);
        
        Assert.That(monsterB.HealthPoints, Is.EqualTo(20f));
    }
}