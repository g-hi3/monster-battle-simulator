namespace MonBatSim.Core;

public class Monster
{
    public void Attack(Monster otherMonster)
    {
        var damage = AttackPoints - otherMonster.DefensePoints;
        if (damage > 0f)
            otherMonster.HealthPoints = ClampZeroOrPositive(otherMonster, damage);
    }

    public MonsterRace Race { get; init; }
    public float HealthPoints { get; set; }
    public float AttackPoints { get; init; }
    public float DefensePoints { get; init; }
    public float Speed { get; init; }

    public bool IsAlive()
    {
        return HealthPoints > 0f;
    }
    
    private static float ClampZeroOrPositive(Monster otherMonster, float damage)
    {
        return Math.Max(otherMonster.HealthPoints - damage, 0f);
    }
}