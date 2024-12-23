using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public MainMenu gameManager; // Reference to the GameManager


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        // Check if shield is active
        PlayerShield playerShield = GetComponent<PlayerShield>();
        if (playerShield != null && playerShield.IsShieldActive())
        {
            Debug.Log("Shield blocked the damage!");
            return; // Damage is blocked
        }

        currentHealth -= amount;
        Debug.Log($"Player took damage! Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Handle player death (respawn, game over, etc.)
    }
}