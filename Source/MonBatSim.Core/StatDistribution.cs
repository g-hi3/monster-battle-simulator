namespace MonBatSim.Core;

public class StatDistribution
{
    private float _healthPoints;
    private float _attackPoints;
    private float _defensePoints;
    private float _speed;

    public StatDistribution(float max)
    {
        Max = max;
    }
        
    public float Max { get; }

    public float HealthPoints
    {
        get => _healthPoints;
        set => _healthPoints = ClampPositiveUpTo(value, Max - AttackPoints - DefensePoints - Speed);
    }

    public float AttackPoints
    {
        get => _attackPoints;
        set => _attackPoints = ClampPositiveUpTo(value, Max - HealthPoints - DefensePoints - Speed);
    }

    public float DefensePoints
    {
        get => _defensePoints;
        set => _defensePoints = ClampPositiveUpTo(value, Max - HealthPoints - AttackPoints - Speed);
    }

    public float Speed
    {
        get => _speed;
        set => _speed = ClampPositiveUpTo(value, Max - HealthPoints - AttackPoints - DefensePoints);
    }

    public float Total => HealthPoints + AttackPoints + DefensePoints + Speed;
    
    private static float ClampPositiveUpTo(float value, float max)
    {
        return Math.Min(Math.Max(value, 0f), max);
    }
}