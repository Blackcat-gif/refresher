using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float jumpSpeed = 10.0f;

    // references
    Rigidbody rb;
    [SerializeField] Animator animator;

    //variables
    Vector3 movementVector;

    [SerializeField] UnityEvent OnJumped;
    [SerializeField] UnityEvent OnCollded;

    [SerializeField] Image healthBar;

    [SerializeField] HealthSO health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health.RestoreHealth();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("walkSpeed", movementVector.magnitude);
    }

    private void FixedUpdate()
    {
        rb.AddForce(movementVector * moveSpeed, ForceMode.Acceleration);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            animator.SetTrigger("jump");
            OnJumped?.Invoke();
        }
    }
    
    public void OnMovement(InputAction.CallbackContext ctx)
    {
        Vector2 inputVector = ctx.ReadValue<Vector2>();
        movementVector = new Vector3(inputVector.x, 0, inputVector.y);

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with solid object named (" + collision.gameObject.name + ")");
        health.DecreaseHealth(3.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Entered trigger with name: {other.name}");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited Trigger");
    }

    private void OnTriggerExit(Collision collision)
    {
        Debug.Log("Exited Collider");
    }
}
