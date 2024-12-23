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
    public float damage = 5f;

    // Detection Range
    public float detectionRange = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = EnemyState.Patrol; // Start with patrol state
    }

    void Update()
    {
        if (player == null) return;

        // Check if the player is within detection range
        if (PlayerInDetectionRange())
        {
            state = EnemyState.Chase; // Switch to chase state
        }
        else if (state == EnemyState.Chase)
        {
            state = EnemyState.Patrol; // Return to patrol if player is out of range
        }

        if (state == EnemyState.Chase)
        {
            Chase();
        }
        else if (state == EnemyState.Patrol)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (path.Length == 0) return;

        // Patrol logic
        if (agent.remainingDistance < distThreshold)
        {
            pathIndex = (pathIndex + 1) % path.Length;
            agent.SetDestination(path[pathIndex].position);
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
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy touched the player!");

            // Apply damage to the player
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
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

        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }

    private void DeathLoot()
    {
        if (collectiblePrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, collectiblePrefabs.Length);
            Instantiate(collectiblePrefabs[randomIndex], transform.position, Quaternion.identity);
        }
    }

    bool PlayerInDetectionRange()
    {
        // Check if the player is within detection range
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= detectionRange;
    }
}