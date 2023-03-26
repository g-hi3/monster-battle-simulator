namespace MonBatSim.Core;

internal class SimpleMonsterBuilder : IMonsterBuilder
{
    private MonsterRace _race;
    protected float HealthPoints { get; private set; }
    protected float AttackPoints { get; private set; }
    protected float DefensePoints { get; private set; }
    protected float Speed { get; private set; }
    
    public virtual Monster Build()
    {
        if (_race == MonsterRace.None)
            throw new InvalidOperationException($"{_race} is not a valid monster race!");

        return new Monster
        {
            Race = _race,
            HealthPoints = ClampZeroOrPositive(HealthPoints),
            AttackPoints = ClampZeroOrPositive(AttackPoints),
            DefensePoints = ClampZeroOrPositive(DefensePoints),
            Speed = ClampZeroOrPositive(Speed),
        };
    }

    public void AssignRace(MonsterRace race)
    {
        _race = race;
    }

    public void AssignHealthPoints(float healthPoints)
    {
        HealthPoints = healthPoints;
    }

    public void AssignAttackPoints(float attackPoints)
    {
        AttackPoints = attackPoints;
    }

    public void AssignDefensePoints(float defensePoints)
    {
        DefensePoints = defensePoints;
    }

    public void AssignSpeed(float speed)
    {
        Speed = speed;
    }

    private static float ClampZeroOrPositive(float value)
    {
        return value > 0f ? value : 0f;
    }
}