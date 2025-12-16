using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float moveSpeed = 10f;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    
    // Components
    private Animator animator;
    private Rigidbody rb;
    
    // State
    private bool isGrounded = true;
    
    void Start()
    {
        // Get components
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
        // If Rigidbody doesn't exist, add one
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        
        // Start running automatically (endless runner)
        rb.linearVelocity = new Vector3(0, 0, moveSpeed);
        
        // Set animation parameters
        animator.SetBool("IsRunning", true);
        animator.SetBool("Running", true);
    }
    
    void Update()
    {
        // Check if player is grounded
        CheckGround();
        
        // Jump when Space is pressed AND grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PerformJump();
        }
    }
    
    void CheckGround()
    {
        // Simple ground check using the groundCheck transform
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }
        else
        {
            // Fallback: raycast from player's position
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.5f, groundLayer);
        }
    }
    
    void PerformJump()
    {
        // Trigger jump animation
        animator.SetTrigger("Jump");
        
        // Apply jump physics
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        
        // Player is now in air
        isGrounded = false;
        
        // Schedule ground check reset (matches jump animation length)
        Invoke("ResetGrounded", 0.8f);
    }
    
    void ResetGrounded()
    {
        isGrounded = true;
    }
    
    // Visual helpers in Scene view
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}