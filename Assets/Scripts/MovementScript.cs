using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class MovementScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private bool facingRight = true;
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Handle horizontal movement input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip character based on direction
        if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
        {
            Flip();
        }

        // Handle jump input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void FixedUpdate()
    {
        // Move the player
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Optional: visualize ground check in editor
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    private void UpdateAnimation()
    {
        bool isIdle = Mathf.Abs(moveInput) < 0.01f;
        bool isRunning = !isIdle && Mathf.Abs(moveInput) > 0.01f;
        bool isJumping = !isGrounded && rb.linearVelocity.y > 0.01f;
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }
}

