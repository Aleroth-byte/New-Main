using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public Transform player; // Reference to the player
    public GameObject projectilePrefab; // The projectile prefab
    public Transform shootPoint; // The point where the projectile will spawn
    public Transform turretHead; // The rotating part of the turret (the head)
    public float detectionRange = 15f; // The detection range within which the turret will target the player
    public float shootRange = 10f; // The range within which the turret will shoot the projectile
    public float fireRate = 1f; // Time between shots
    public float rotationSpeed = 5f; // Speed of the turret's head rotation towards the player

    private float nextFireTime = 0f;

    private void Update()
    {
        if (player == null) return;

        // Calculate the distance from the turret to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = player.position - turretHead.position; // Make the turret head rotate towards the player
            directionToPlayer.y = 0f; // Keep the rotation level (no pitch)

            // Smoothly rotate the turret head to face the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            turretHead.rotation = Quaternion.Slerp(turretHead.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // If the player is within shooting range, fire the projectile
            if (distanceToPlayer <= shootRange && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate; // Adjust fire rate
            }
        }
    }

    void Shoot()
    {
        // Spawn the projectile in front of the turret (in the direction the turret head is facing)
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, turretHead.rotation);

        // Optional: Add a Rigidbody component if it's not present and give it velocity in the forward direction
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = turretHead.forward * 10f; // Set the speed of the projectile
        }

        // Debug to check where the projectile spawns
        Debug.Log("Projectile spawned at: " + shootPoint.position);
    }
}