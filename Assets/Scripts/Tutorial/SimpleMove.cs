using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    private Rigidbody rb;
    private Vector2 moveInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed;
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }
}
