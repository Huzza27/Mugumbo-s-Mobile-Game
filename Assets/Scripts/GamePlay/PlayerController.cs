using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public DynamicJoystick joystick; // Reference to your joystick
    public float speed = 5f;         // Movement speed
    public float rotationSpeed = 10f; // Speed for smooth rotation

    private Rigidbody rb;            // Reference to Rigidbody for physics-based movement

    [SerializeField] PlayAnimatorController animController;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get input from the joystick
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        // Create a movement vector based on joystick input
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Apply movement
        if (direction.magnitude >= 0.1f)
        {
            PlayAnimatorController.SetBoolForAnimator(animator, true, "isMoving");
            rb.MovePosition(rb.position + -direction * speed * Time.fixedDeltaTime);

            // Rotate the player to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(-direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
        else
        {
            PlayAnimatorController.SetBoolForAnimator(animator, false, "isMoving");
        }
    }
}
