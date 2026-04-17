using UnityEngine;

namespace Main_Menu
{
    public class SettingsMenu : MonoBehaviour
    {
        public GameObject mainMenuPanel;
        public GameObject optionsPanel;
    
        void Start()
        {
            optionsPanel.SetActive(false);
        }

        public void goBackToMainMenu()
        {
            mainMenuPanel.SetActive(true);
            optionsPanel.SetActive(false);
        }
    }
}
