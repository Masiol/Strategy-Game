using System.Collections;
using UnityEngine;

public class Infantry : Unit
{
    private int randomAttack;

    protected override void Awake()
    {
        base.Awake();       
        StartCoroutine(RandomAttack());
    }

    protected override void Attack()
    {
        animator.SetInteger("Attack_Int", randomAttack);
        animator.SetBool("Attack", true);
    }

    IEnumerator RandomAttack()
    {
        while (true)
        {
            randomAttack = Random.Range(0, 2);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void PerformAttack()
    {
        GetComponent<IColliderAttack>().CalculateAttackCollider();
    }
}
