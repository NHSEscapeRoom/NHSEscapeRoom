using UnityEngine;

public class CreditActivator : MonoBehaviour
{
    public GameObject regularPanel;
    public GameObject creditsPanel;
    
    void Start()
    {
        regularPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void creditActivator()
    {
        regularPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
}
