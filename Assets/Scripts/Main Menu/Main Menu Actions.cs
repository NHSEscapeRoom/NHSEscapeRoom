using UnityEngine;
using UnityEngine.Rendering;

public class MainMenuActions : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;
    public GameObject levelsPanel;
    
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OpenLevelSelector()
    {
        mainMenuPanel.SetActive(false);
        levelsPanel.SetActive(true);
    }
    
    /// <summary>
    /// Quits the game (with a successful state)...
    /// This is for the quit game button(s) on the main menu.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
