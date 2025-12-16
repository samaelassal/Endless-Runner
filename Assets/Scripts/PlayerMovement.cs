using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float playerSpeed = 10f;
    public float laneChangeSpeed = 10f;

    [Header("Lane Limits")]
    public float leftLimit = -5f;
    public float rightLimit = 5f;

    [Header("Jump")]
    public float jumpForce = 6f;

    private int currentLane = 1;
    private float[] lanes = new float[3];

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded = true;

    void Start()
    {
        lanes[0] = leftLimit;
        lanes[1] = 0f;
        lanes[2] = rightLimit;

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveForward();
        HandleInput();
        MoveToLane();
        HandleJump();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * playerSpeed * Time.deltaTime, Space.World);
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentLane--;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentLane++;
        }

        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }

    void MoveToLane()
    {
        Vector3 targetPosition = new Vector3(
            lanes[currentLane],
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            laneChangeSpeed * Time.deltaTime
        );
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            animator.SetTrigger("Jump");
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
