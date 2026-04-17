using UnityEngine;
using UnityEngine.InputSystem;

namespace Tutorial
{
    public class SimpleMove : MonoBehaviour
    {
        public float speed = 5f;
        private Vector2 moveInput;
        private CharacterController controller;

        void Awake() => controller = GetComponent<CharacterController>();

        public void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
        }

        void Update()
        {
            Vector3 direction = transform.right * moveInput.x + transform.forward * moveInput.y;
            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}