using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Config params
    [SerializeField] float maxHealth = 5.0f;

    // Cached refs
    private float currentHealth;

    // Events
    public event Action onDeath;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void HealFully()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealthPercentage()
    {
        return currentHealth/maxHealth;
    }

    private void Die()
    {
        if (onDeath != null)
        {
            onDeath.Invoke();
        }
        // Death behaviour
        Destroy(this.gameObject);
    }
}
