namespace MonBatSim.Core;

public class MonsterArmy
{
    private readonly Monster[] _monsters;

    public MonsterArmy(IEnumerable<Monster> monsters)
    {
        _monsters = monsters.ToArray();
    }

    public float HealthPoints => _monsters.Sum(monster => monster.HealthPoints);
    public float AttackPoints => HighestOfAllMonsters(monster => monster.AttackPoints);
    public float DefensePoints => HighestOfAllMonsters(monster => monster.DefensePoints);
    public float Speed => _monsters.Average(monster => monster.Speed);

    private float HighestOfAllMonsters(Func<Monster, float> selector)
    {
        return _monsters.Max(selector);
    }

    public void Attack(MonsterArmy otherArmy)
    {
        var attackers = _monsters
            .Where(monster => monster.IsAlive())
            .OrderByDescending(monster => monster.Speed)
            .ThenByDescending(monster => monster.AttackPoints);
        var defenders = otherArmy._monsters
            .OrderByDescending(monster => monster.DefensePoints)
            .ThenByDescending(monster => monster.HealthPoints)
            .ToArray();

        foreach (var attacker in attackers)
        {
            var nextDefender = defenders.FirstOrDefault(monster => monster.IsAlive());
            if (nextDefender != default)
                attacker.Attack(nextDefender);
            else
                return;
        }
    }
}