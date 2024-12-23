using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Is this the final collectible?")]
    public bool isFinalCollectible = false;

    private void OnTriggerEnter(Collider other)
    {
        // Ensure only the player can collect the item
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collected an item!");

            CollectibleSystem collectibleSystem = FindObjectOfType<CollectibleSystem>();

            if (collectibleSystem != null)
            {
                if (isFinalCollectible)
                {
                    collectibleSystem.WinGame();
                }
                else
                {
                    collectibleSystem.CollectItem();
                }
            }

            // Destroy this collectible
            Destroy(gameObject);
        }
    }
}