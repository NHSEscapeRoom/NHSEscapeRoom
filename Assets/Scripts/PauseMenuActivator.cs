using System;
using UnityEngine;

public class PauseMenuActivator : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject genericUI;
    
    public void ShowMainMenu()
    {
        pauseMenu.SetActive(true);
        genericUI.SetActive(false);
    }

    public void Start()
    {
        pauseMenu.SetActive(false);
    }
}
