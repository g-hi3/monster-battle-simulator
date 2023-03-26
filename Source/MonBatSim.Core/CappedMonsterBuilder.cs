namespace MonBatSim.Core;

internal class CappedMonsterBuilder : SimpleMonsterBuilder
{
    private readonly float _pointsCap;

    public CappedMonsterBuilder(float pointsCap)
    {
        _pointsCap = pointsCap;
    }

    private float StatTotal => HealthPoints + AttackPoints + DefensePoints + Speed;

    public override Monster Build()
    {
        if (_pointsCap < StatTotal)
            throw new InvalidOperationException(
                $"Stat total of {StatTotal} may not is higher than allowed {_pointsCap}!");
        
        return base.Build();
    }
}