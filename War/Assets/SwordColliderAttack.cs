using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderAttack : MonoBehaviour, IColliderAttack
{
    public Transform hitPoint;
    public float hitRadius;
    private DamageController damageController;
    private void Awake()
    {
        damageController = new DamageController();
    }
    public void CalculateAttackCollider()
    {
        Collider[] hitColliders = Physics.OverlapSphere(hitPoint.position, hitRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag(GetComponent<Unit>().enemyTag))
            {
                // Tylko jedna jednostka przeciwnika jest atakowana
                float damage = damageController.CalculateDamage(GetComponent<Unit>().unit.TotalDamage); 
                damageController.ApplyDamage(hitCollider.gameObject, damage);
                break;
            }
        }
        Debug.Log("Sword attack performed!");
    }
    private void OnDrawGizmos()
    {
        if (hitPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitPoint.position, hitRadius);
        }
    }
}
