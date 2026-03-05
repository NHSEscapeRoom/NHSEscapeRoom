using UnityEngine;

public class MainMenuActions : MonoBehaviour
{
    /// <summary>
    /// Quits the game (with a successful state)...
    /// This is for the quit game button(s) on the main menu.
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
