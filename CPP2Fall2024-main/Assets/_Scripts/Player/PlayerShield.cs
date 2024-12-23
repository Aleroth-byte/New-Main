using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    public GameObject shieldPrefab; // Assign the shield prefab
    private GameObject activeShield; // To track the active shield instance
    public float shieldDuration = 5f; // How long the shield lasts

    private bool isShieldActive = false;

    public void ActivateShield()
    {
        if (isShieldActive) return; // Prevent multiple activations

        Debug.Log("Shield activated!");
        isShieldActive = true;

        // Instantiate and position the shield
        Vector3 shieldPosition = transform.position;
        shieldPosition.y -= 1f; // Adjust this offset as needed to align with the player
        activeShield = Instantiate(shieldPrefab, shieldPosition, Quaternion.identity, transform);

        // Start the shield timer
        Invoke(nameof(DeactivateShield), shieldDuration);
    }

    private void DeactivateShield()
    {
        Debug.Log("Shield deactivated!");
        isShieldActive = false;

        if (activeShield != null)
        {
            Destroy(activeShield); // Remove the shield
        }
    }

    // Block damage while shield is active
    public bool IsShieldActive()
    {
        return isShieldActive;
    }
}
