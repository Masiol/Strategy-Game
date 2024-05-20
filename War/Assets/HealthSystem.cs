using System;
using TMPro;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
   
    public bool isDead = false;
    public event Action<float> OnHealthChanged;
    public event Action OnDeath; 
    
    [HideInInspector] public float maxHealth;
    private float currentHealth;

    private void Awake()
    {
        maxHealth = GetComponent<Unit>().unit.TotalHealth;
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(float _damage)
    {
        if (isDead) return;

        currentHealth -= _damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float _amount)
    {
        if (isDead) return;

        currentHealth += _amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth);
    }

    private void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
  
        //gameObject.SetActive(false);
    }
}
