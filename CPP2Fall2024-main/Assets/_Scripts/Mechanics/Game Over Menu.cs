using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{


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
}

    // Method to initialize the buttons (called when the script starts)