using UnityEngine;

public class DamageController
{
    public float CalculateDamage(float _baseDamage, float _modifier = 1)
    {
        return _baseDamage * _modifier;
    }

    public void ApplyDamage(GameObject _target, float _damage)
    {
        HealthSystem healthSystem = _target.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(_damage);
        }
    }
}
