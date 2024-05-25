using System.Collections;
using UnityEngine;

public class Infantry : Unit
{
    public int randomAttack;

    protected override void Awake()
    {
        base.Awake();       
    }

    public override void Attack()
    {
        StartCoroutine(RandomAttack());
        base.SetAnimation("Attack", true);
    }

    IEnumerator RandomAttack()
    {
        while (true)
        {
            randomAttack = Random.Range(0, 2);
           // animator.SetInteger("Attack_Int", randomAttack);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void PerformAttack()
    {
        GetComponent<IColliderAttack>().CalculateAttackCollider();
    }
}
