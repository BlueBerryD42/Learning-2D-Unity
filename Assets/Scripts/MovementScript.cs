using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class MovementScript : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpForce = 10f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private bool facingRight = true;
    private Animator animator;

    private bool isDead = false;
    private bool isAttacking = false;
    private bool isRunning = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        moveInput = Input.GetAxisRaw("Horizontal");

        // Run logic (hold shift)
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Handle jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            animator.SetTrigger("Jump");
        }

        // Handle attack
        if (Input.GetMouseButtonDown(0)) // Left click to attack
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }

        // Handle death
        if (Input.GetKeyDown(KeyCode.K)) // Press 'K' to simulate death
        {
            isDead = true;
            animator.SetTrigger("Die");
            rb.linearVelocity = Vector2.zero;
        }

        FlipCharacter();
        UpdateAnimation();
        DebugMovementState();
    }

    void FixedUpdate()
    {
        if (isDead) return;

        float speed = isRunning ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void FlipCharacter()
    {
        if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void UpdateAnimation()
    {
        bool isIdle = Mathf.Abs(moveInput) < 0.01f && isGrounded && !isAttacking;
        bool isWalking = Mathf.Abs(moveInput) > 0.01f && !isRunning && isGrounded;
        bool isJumping = !isGrounded && rb.linearVelocity.y > 0.1f;
        bool isFalling = !isGrounded && rb.linearVelocity.y < -0.1f;

        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning && Mathf.Abs(moveInput) > 0.01f && isGrounded);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isFalling", isFalling);
    }

    private void DebugMovementState()
    {
        Debug.Log($"[MovementState] Grounded: {isGrounded}, MoveInput: {moveInput}, Velocity: {rb.linearVelocity}, Dead: {isDead}, Running: {isRunning}");
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
