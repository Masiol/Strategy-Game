using UnityEngine;

[CreateAssetMenu(menuName = "FSM/States/DiedState")]
public class DiedState : State
{
    public override void Enter(Unit unit)
    {
        int randomDeath = Random.Range(0, 2);
        unit.animator.SetInteger("Death_Int", randomDeath);
        unit.animator.SetTrigger("Death");
        unit.SetAnimation("Died", true);

        unit.GetComponent<Collider>().enabled = false;
        unit.agent.enabled = false;
        unit.enabled = false;

    }

    public override void Execute(Unit unit)
    {
        //throw new System.NotImplementedException();
    }

    public override void Exit(Unit unit)
    {
        //throw new System.NotImplementedException();
    }
}
