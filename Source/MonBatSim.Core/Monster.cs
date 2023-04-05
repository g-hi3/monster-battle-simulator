namespace MonBatSim.Core;

public class Monster
{
    public string Name { get; set; }
    public MonsterRace Race { get; init; }
    public float HealthPoints { get; set; }
    public float AttackPoints { get; init; }
    public float DefensePoints { get; init; }
    public float Speed { get; init; }

    public void Attack(Monster otherMonster)
    {
        var damage = AttackPoints - otherMonster.DefensePoints;
        if (damage > 0f)
            otherMonster.HealthPoints = ClampZeroOrPositive(otherMonster, damage);
    }
    
    public bool IsAlive()
    {
        return HealthPoints > 0f;
    }
    
    private static float ClampZeroOrPositive(Monster otherMonster, float damage)
    {
        return Math.Max(otherMonster.HealthPoints - damage, 0f);
    }

    public static Monster Create(string name, MonsterRace race, StatDistribution stats)
    {
        return new Monster
        {
            Name = name,
            Race = race,
            HealthPoints = stats.HealthPoints,
            AttackPoints = stats.AttackPoints,
            DefensePoints = stats.DefensePoints,
            Speed = stats.Speed
        };
    }
}