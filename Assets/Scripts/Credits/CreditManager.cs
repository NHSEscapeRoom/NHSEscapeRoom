using UnityEngine;

namespace Credits
{
    public class CreditManager : MonoBehaviour
    {
        public GameObject regularPanel;
        public GameObject creditsPanel;
        
        public void closeCredits()
        {
            regularPanel.SetActive(true);
            creditsPanel.SetActive(false);
        }
    }
}