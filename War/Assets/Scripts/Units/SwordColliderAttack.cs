using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderAttack : MonoBehaviour, IColliderAttack
{
    public GameObject sword; // Miecz
    public Transform hitPoint; // Punkt trafienia
    private DamageController damageController;
    private Collider swordCollider;

    private void Awake()
    {
        damageController = new DamageController();
        // Pobierz komponent Collider przypiêty do miecza
        swordCollider = sword.GetComponent<Collider>();
        // Wy³¹cz collider na pocz¹tku
        swordCollider.enabled = false;
    }

    public void CalculateAttackCollider()
    {
        swordCollider.enabled = true;
        Collider[] hitColliders = Physics.OverlapBox(swordCollider.bounds.center, swordCollider.bounds.extents, swordCollider.transform.rotation);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag(GetComponent<Unit>().enemyTag))
            {
                Unit enemy = hitCollider.gameObject.GetComponent<Unit>();

                if (enemy != null)
                {
                    enemy.GetComponent<HealthSystem>().TakeDamage(damageController.CalculateDamage(GetComponent<Unit>().unitBase.TotalDamage));
                    swordCollider.enabled = false;

                }
            }
        }
    }
}
