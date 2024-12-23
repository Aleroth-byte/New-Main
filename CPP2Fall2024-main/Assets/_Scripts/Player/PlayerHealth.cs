using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth; // Initialize health
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"TakeDamage called. Damage: {damage}");
        currentHealth -= damage;
        Debug.Log($"Current health after damage: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player has died!");
        Destroy(gameObject); // Destroy the player GameObject
    }
}