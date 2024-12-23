using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectibleSystem : MonoBehaviour
{
    [Header("Collectibles")]
    public int totalCollectibles = 4; // Total number of collectibles
    private int collectedCount = 0;  // Tracks collected items

    [Header("Final Collectible")]
    public GameObject finalCollectible; // Reference to the final collectible in the scene

    [Header("UI Feedback (Optional)")]
    public TMPro.TextMeshProUGUI collectibleCounterText; // Assign a UI text element for collectible counter

    [Header("Win Condition")]
    public string winSceneName = "WinScene"; // The name of the scene to load when the player wins

    private void Start()
    {
        // Ensure the final collectible is initially invisible
        if (finalCollectible != null)
        {
            finalCollectible.SetActive(false);
        }

        UpdateCollectibleCounter();
    }

    public void CollectItem()
    {
        collectedCount++;
        UpdateCollectibleCounter();

        if (collectedCount == totalCollectibles - 1 && finalCollectible != null)
        {
            // Activate the final collectible
            finalCollectible.SetActive(true);
            Debug.Log("Final collectible is now visible!");
        }
    }

    private void UpdateCollectibleCounter()
    {
        if (collectibleCounterText != null)
        {
            collectibleCounterText.text = $"Collectibles: {collectedCount}/{totalCollectibles}";
        }
    }

    public void WinGame()
    {
        Debug.Log("You collected the final item! You win!");

        // Load the win scene or trigger a win event
        if (!string.IsNullOrEmpty(winSceneName))
        {
            SceneManager.LoadScene(winSceneName);
        }
        else
        {
            Debug.Log("Win scene not specified. Game won!");
        }
    }
}