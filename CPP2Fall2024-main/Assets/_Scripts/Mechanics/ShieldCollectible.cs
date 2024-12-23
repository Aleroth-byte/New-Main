using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Shield collectible picked up!");

            // Activate the shield on the player
            PlayerShield playerShield = other.GetComponent<PlayerShield>();
            if (playerShield != null)
            {
                playerShield.ActivateShield();
            }

            // Destroy the collectible
            Destroy(gameObject);
        }
    }
}