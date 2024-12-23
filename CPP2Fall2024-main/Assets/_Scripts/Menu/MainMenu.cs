using UnityEngine;
using UnityEngine.SceneManagement;  // To load scenes
using UnityEngine.UI;  // To interact with UI elements

public class MainMenu : MonoBehaviour
{
    // Reference to buttons (optional if you prefer dragging directly in inspector)
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public Button creditsButton;
    public GameObject mainMenu;


    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // Method to start the game
    public void PlayGame()
    {
        // Loads the scene with index 1 (assuming the main game scene is at index 1 in the Build Settings)
        SceneManager.LoadScene("GameTime");  // Change to your game's scene index or name
    }

    public void ShowCredits()
    {
        // Loads the scene with index 1 (assuming the main game scene is at index 1 in the Build Settings)
        SceneManager.LoadScene("tut");  // Change to your game's scene index or name
    }
    // Method to show options (for now it just prints a message)
    public void ShowOptions()
    {
        Debug.Log("Options button clicked!");

        // Loads the scene with index 1 (assuming the main game scene is at index 1 in the Build Settings)
        SceneManager.LoadScene("settingsMenu");  // Change to your game's scene index or name
    }


    // Method to quit the game
    public void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit();

        // For Unity editor, play mode must be stopped manually:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}    // Method to initialize the buttons (called when the script starts)
