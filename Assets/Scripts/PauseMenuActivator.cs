using System;
using UnityEngine;

public class PauseMenuActivator : MonoBehaviour
{
    public GameObject pauseMenu;
    
    public void ShowMainMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void Start()
    {
        pauseMenu.SetActive(false);
    }
}
