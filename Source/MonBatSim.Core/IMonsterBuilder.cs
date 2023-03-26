namespace MonBatSim.Core;

public interface IMonsterBuilder
{
    Monster Build();
    void AssignRace(MonsterRace race);
    void AssignHealthPoints(float healthPoints);
    void AssignAttackPoints(float attackPoints);
    void AssignDefensePoints(float defensePoints);
    void AssignSpeed(float speed);
}