using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class KeypadUIActivator : MonoBehaviour
{
    public GameObject keypad;
    [FormerlySerializedAs("Panel")] public GameObject panel;
    
    public GameObject defaultPanel;
    
    // Autoclose the Keypad. We don't need it unless the user clicks on it.
    void Start()
    {
        panel.SetActive(false);
    }

    void Update()
    {
        Vector2 inputPosition = Vector2.zero;
        bool inputPressed = false;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            inputPosition = Mouse.current.position.ReadValue();
            inputPressed = true;
        }
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            inputPressed = true;
        }

        if (inputPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(inputPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == keypad)
                {
                    panel.SetActive(true);
                    defaultPanel.SetActive(false);
                }
            }
        }
    }
}
