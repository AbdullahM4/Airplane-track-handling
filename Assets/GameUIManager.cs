using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    // UI Panels
    public GameObject winPanel;
    public GameObject losePanel;

    // Show the win panel and hide the lose panel
    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        losePanel.SetActive(false);
    }

    // Show the lose panel and hide the win panel
    public void ShowLosePanel()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(true);
    }

    // Hide both win and lose panels
    public void HideAllPanels()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    // Restart the current level
    public void RestartLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Load Scenario 1
    public void LoadScenario1()
    {
        // Load the scene with build index 1
        SceneManager.LoadScene(1);
    }

    // Load Scenario 2
    public void LoadScenario2()
    {
        // Load the scene with build index 2
        SceneManager.LoadScene(2);
    }

    // Go back to the main menu
    public void BackToMainMenu()
    {
        // Load the main menu scene (assuming it's scene index 0)
        SceneManager.LoadScene(0);
    }

    // Quit the application
    public void QuitGame()
    {
        Application.Quit();
    }

    // Option method for handling options (e.g., settings)
    public void Options()
    {
        // Implement options functionality here
        Debug.Log("Options button clicked - implement options UI here.");
    }
}
