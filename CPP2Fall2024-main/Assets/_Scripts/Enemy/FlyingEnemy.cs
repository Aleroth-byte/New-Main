using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed = 5f; // Movement speed
    public float detectionRange = 10f; // Detection range
    public Transform player; // Reference to the player

    [Header("Special Effect")]
    public GameObject specialEffectPrefab; // Special effect prefab to spawn on death

    [Header("Enemy Stats")]
    public float damageToPlayer = 10f; // Damage dealt to the player

    private bool isChasing = false; // Whether the enemy is chasing the player

    void Update()
    {
        if (player == null) return;

        // Check if the player is within detection range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // If chasing, move toward the player
        if (isChasing)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        // Move toward the player's position
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Rotate to face the player
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Flying enemy touched the player!");

            // Apply damage to the player
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }

            // Trigger the special effect
            if (specialEffectPrefab != null)
            {
                Instantiate(specialEffectPrefab, transform.position, Quaternion.identity);
            }

            // Destroy the enemy
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Fallback in case OnCollision doesn't detect properly
        if (other.CompareTag("Player"))
        {
            Debug.Log("Flying enemy triggered with the player!");

            // Apply damage to the player
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }

            // Trigger the special effect
            if (specialEffectPrefab != null)
            {
                Instantiate(specialEffectPrefab, transform.position, Quaternion.identity);
            }

            // Destroy the enemy
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection range in the scene view for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
