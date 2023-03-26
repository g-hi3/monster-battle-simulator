namespace MonBatSim.Core;

public class MonsterArmy
{
    private readonly Monster[] _monsters;

    public MonsterArmy(IEnumerable<Monster> monsters)
    {
        _monsters = monsters.ToArray();
    }

    public float HealthPoints => SumOfAllMonsters(monster => monster.HealthPoints);
    public float AttackPoints => SumOfAllMonsters(monster => monster.AttackPoints);
    public float DefensePoints => SumOfAllMonsters(monster => monster.DefensePoints);
    public float Speed => _monsters.Average(monster => monster.Speed);

    private float SumOfAllMonsters(Func<Monster, float> selector) => _monsters.Sum(selector);
}