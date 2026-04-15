using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject levelsPanel;

    void Start()
    {
        levelsPanel.SetActive(false);
    }
    
    public void goBackToMainMenuFromLevelSelect()
    {
        mainMenuPanel.SetActive(true);
        levelsPanel.SetActive(false);
    }

    public void goToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void goToLevelOne()
    {
        SceneManager.LoadScene("Level 1");
    }
}
