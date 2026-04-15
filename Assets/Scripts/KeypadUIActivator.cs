using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class KeypadUIActivator : MonoBehaviour
{
    public GameObject keypad;
    [FormerlySerializedAs("Panel")] public GameObject panel;
    
    // Autoclose the Keypad. We don't need it unless the user clicks on it.
    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == keypad)
                {
                    panel.SetActive(true);
                }
            }
        }
    }
    
    public void ClosePanel()
    {
        panel.SetActive(false);
    }

}
