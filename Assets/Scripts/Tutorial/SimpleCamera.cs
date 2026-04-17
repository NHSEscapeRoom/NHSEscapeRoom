using UnityEngine;
using UnityEngine.InputSystem;

namespace Tutorial
{
    public class SimpleCamera : MonoBehaviour
    {
        public Transform playerBody;
        public float sensitivity = 3f;
        public float minPitch = -30f;
        public float maxPitch = 60f;

        private float pitch = 0f;
        private Vector2 lookInput;

        public void OnLook(InputValue value)
        {
            lookInput = value.Get<Vector2>();
        }

        void LateUpdate()
        {
            playerBody.Rotate(Vector3.up * lookInput.x * sensitivity);

            pitch -= lookInput.y * sensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
            transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        }
    }
}