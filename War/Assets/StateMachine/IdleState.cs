using UnityEngine;

[CreateAssetMenu(menuName = "FSM/States/IdleState")]
public class IdleState : State
{
    public override void Enter(Unit unit)
    {
        unit.animator.SetBool("Idle", true);
    }

    public override void Execute(Unit unit)
    {
        // Do nothing or check for conditions to transition to other states
    }

    public override void Exit(Unit unit)
    {
        unit.animator.SetBool("Idle", false);
    }
}
