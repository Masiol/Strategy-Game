using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearColliderAttack : MonoBehaviour, IColliderAttack
{
    [SerializeField] private Transform capsuleTop;
    [SerializeField] private Transform capsuleBottom;
    [SerializeField] private float capsuleRadius;
    private DamageController damageController;
    private void Awake()
    {
        damageController = new DamageController();
    }
    public void CalculateAttackCollider()
    {

        // Logika specyficzna dla ataku w³óczni¹
        Vector3 topPosition = capsuleTop.transform.position;
        Vector3 bottomPosition = capsuleBottom.transform.position;
        Collider[] hitColliders = Physics.OverlapCapsule(topPosition, bottomPosition, capsuleRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag(GetComponent<Unit>().enemyTag))
            {
                // W³ócznia mo¿e atakowaæ kilka jednostek naraz
                float damage = damageController.CalculateDamage(GetComponent<Unit>().unit.TotalDamage); 
                damageController.ApplyDamage(hitCollider.gameObject, damage);
            }
        }
        Debug.Log("Spear attack performed!");

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 topPosition = capsuleTop.transform.position;
        Vector3 bottomPosition = capsuleBottom.transform.position;
        Gizmos.DrawWireSphere(topPosition, capsuleRadius);
        Gizmos.DrawWireSphere(bottomPosition, capsuleRadius);

        Gizmos.DrawLine(new Vector3(topPosition.x, topPosition.y - capsuleRadius, topPosition.z),
                        new Vector3(bottomPosition.x, bottomPosition.y + capsuleRadius, bottomPosition.z));
    }
}

