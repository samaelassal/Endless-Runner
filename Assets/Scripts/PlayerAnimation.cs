using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Animator animator;
    private bool isJumping = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            isJumping = true;
            animator.SetTrigger("Jump");

            // Allow jump again after animation
            Invoke(nameof(ResetJump), 1f);
        }
    }

    void ResetJump()
    {
        isJumping = false;
    }
}
