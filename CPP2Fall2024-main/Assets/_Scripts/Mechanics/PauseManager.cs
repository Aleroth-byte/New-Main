using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel; // UI Panel for the pause menu
    private bool isPaused = false;

    void Start()
    {
        // Ensure the game starts unpaused and the panel is hidden
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Toggle pause when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true); // Show the pause panel
        Time.timeScale = 0f; // Freeze the game
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false); // Hide the pause panel
        Time.timeScale = 1f; // Resume the game
        Cursor.visible = false; // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
    }
}