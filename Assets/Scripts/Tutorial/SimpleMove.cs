using UnityEngine;
using UnityEngine.InputSystem;

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
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(direction * speed * Time.deltaTime);
    }
}