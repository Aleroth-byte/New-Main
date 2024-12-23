using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [Header("Loot Settings")]
    public GameObject[] collectiblePrefabs;
    public Transform spawnPosition;

    public Transform player;
    public GameObject healthDropPrefab; // Prefab for the health drop

    Transform target;
    NavMeshAgent agent;

    public enum EnemyState
    {
        Chase,
        Patrol
    }

    public EnemyState state;

    public Transform[] path;
    public int pathIndex = 0;
    public float distThreshold = 0.2f; // Distance threshold for patrol points

    // Enemy stats
    public float health = 100f;

    // Damage the enemy deals when colliding with the player
    public float damage = 5f; // Adjust this value to control the enemy's damage

    // Detection Range
    public float detectionRange = 10f; // Detection range radius

    private SphereCollider detectionCollider; // To handle detection range

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Set up detection sphere
        detectionCollider = gameObject.AddComponent<SphereCollider>();
        detectionCollider.isTrigger = true;
        detectionCollider.radius = detectionRange;
    }

    void Update()
    {
        if (player == null) return;

        else if (state == EnemyState.Chase)
        {
            Chase();
        }
    }


    void Chase()
    {
        target = player.transform;
        agent.SetDestination(target.position);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy touched the player!");

            // Apply damage to the player and handle player death
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Deal the configured damage to the player
            }

            // No longer destroy the enemy here
            // The enemy will keep existing and continue to chase/patrol
        }

        // Check if the enemy collides with the player's weapon
        Weapon weapon = collision.gameObject.GetComponent<Weapon>();
        if (weapon != null)
        {
            // Apply damage to the enemy
            TakeDamage(10f); // Assuming a fixed damage value of 10 from the weapon
            Debug.Log("Enemy damaged by weapon!");
        }
    }

    void Die()
    {
        // Drop health pickup
        if (healthDropPrefab != null)
        {
            Instantiate(healthDropPrefab, transform.position, Quaternion.identity);
        }

        // Drop loot after death
        DeathLoot();

        // Play death effects (optional)
        Debug.Log("Enemy died!");

        // Destroy the enemy
        Destroy(gameObject); // Adjust the delay as needed
    }

    private void DeathLoot()
    {
        // Spawn a random collectible at the ghost's position
        if (collectiblePrefabs.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, collectiblePrefabs.Length);
            Vector3 spawnPosition = transform.position;

            Instantiate(collectiblePrefabs[randomIndex], spawnPosition, Quaternion.identity);

            Debug.Log($"Spawned {collectiblePrefabs[randomIndex].name} at {spawnPosition}");
        }
        else
        {
            Debug.LogError("No collectibles available to spawn.");
        }
    }

    // Detect the player entering the detection range
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = EnemyState.Chase; // Start chasing the player
            Debug.Log("Player detected! Chasing...");
        }
    }

    // Detect the player exiting the detection range (optional, to return to patrol)
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            state = EnemyState.Patrol; // Return to patrol state
            Debug.Log("Player out of range. Returning to patrol...");
        }
    }
}