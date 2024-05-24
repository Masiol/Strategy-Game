using UnityEngine;

[CreateAssetMenu(menuName = "FSM/States/AttackState")]
public class AttackState : State
{
    private float rangeThreshold;
    [SerializeField] private float minThreshold;
    [SerializeField] private float maxThreshold;
    public override void Enter(Unit unit)
    {
        unit.Attack();
        rangeThreshold = maxThreshold;
    }

    public override void Execute(Unit unit)
    {
        unit.FaceTarget(unit.enemyObject.transform.position);

        if (Vector3.Distance(unit.transform.position, unit.enemyObject.transform.position) >= unit.unitBase.attackRange + rangeThreshold)
        {
            unit.TransitionToState(new MoveToEnemyState());
        }
    }
     

    public override void Exit(Unit unit)
    {
        unit.SetAnimation("Attack", false);
    }
}
