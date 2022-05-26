using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Config params
    [SerializeField] float maxHealth = 5.0f;

    // Cached refs
    private float currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{name} took damage! Health = {currentHealth}/{maxHealth}");
        
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
        // Death behaviour
        Debug.Log($"{name} died.");
        Destroy(this.gameObject);
    }
}
