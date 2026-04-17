using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Tutorial
{
    public class BookUIActivator : MonoBehaviour
    {
        public GameObject book;

        public GameObject bookPanel;
        public GameObject regularPanel;

        void Start()
        {
            bookPanel.SetActive(false);
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
                
                
                if (Camera.main == null) return;

                Ray ray = Camera.main.ScreenPointToRay(inputPosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject == book)
                    {
                        bookPanel.SetActive(true);
                        regularPanel.SetActive(false);
                    }
                }
            }
        }
    }
}