using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/States/AttackState")]
public class AttackState : State
{
    public override void Enter(Unit unit)
    {
        unit.Attack();
        unit.agent.isStopped = true;
    }

    public override void Execute(Unit unit)
    {
        if(unit.enemyObject != null)
        unit.FaceTarget(unit.enemyObject.transform.position);

        if (Vector3.Distance(unit.transform.position, unit.enemyObject.transform.position) >= unit.unitBase.attackRange)
        {
            unit.TransitionToState(unit.moveToEnemyState);
        }
    }
     

    public override void Exit(Unit unit)
    {
        unit.SetAnimation("Attack", false);
        unit.agent.isStopped = false;
    }
}
