using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/States/MoveToEnemyState")]
public class MoveToEnemyState : State
{
    public float stopDistance = 2.0f;

    public override void Enter(Unit unit)
    {
        unit.SetAnimation("Walk", true);
        unit.agent.isStopped = false;
    }

    public override void Execute(Unit unit)
    {
        GameObject enemy = unit.enemyObject;
        if (enemy)
        {
            unit.FaceTarget(enemy.transform.position);
            if (Vector3.Distance(unit.transform.position, enemy.transform.position) > unit.unitBase.attackRange)
            {

                unit.agent.SetDestination(enemy.transform.position);
            }
            else
            {
                unit.agent.isStopped = true;
                unit.TransitionToState(new AttackState());
            }
        }
    }

    public override void Exit(Unit unit)
    {
        unit.SetAnimation("Walk", false);
        unit.agent.isStopped = true;
    }
}
