using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f; // Speed of the projectile
    public float damage = 20f; // Damage dealt to the player or enemy
    public float lifeTime = 5f; // Time before the projectile self-destructs

    private Rigidbody rb;

    private void Start()
    {
        // Ensure the projectile has a Rigidbody for movement (set to Kinematic if needed)
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // We don't need physics forces, just movement
        }

        // Destroy the projectile after a set time
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Move the projectile forward in the direction it was shot
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Log the object the projectile collided with
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Projectile hit the player!");

            // If the player has a Health component, apply damage
            Health playerHealth = other.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Deal damage to player
            }

            // Destroy the projectile after hitting the player
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Projectile hit an enemy!");

            // If the enemy has an Enemy component, apply damage
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Deal damage to enemy
            }

            // Destroy the projectile after hitting the enemy
            Destroy(gameObject);
        }
        else
        {
            // Destroy the projectile if it hits anything else
            Destroy(gameObject);
        }
    }
}