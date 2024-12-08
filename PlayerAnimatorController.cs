using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public Animator animator; // reference to the Animator
    private Rigidbody rb;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // calculatate Speed
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized;
        float speed = movement.magnitude;

        // update Speed Parameter
        animator.SetFloat("Speed", speed);

        // Handle Jump Input
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, 5f, rb.velocity.z); 
            animator.SetBool("IsJumping", true);
        }
    }

    private bool IsGrounded()
    {
        
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
// Reset jump state on landing
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("IsJumping", false); 
        }
    }

    //  Superspeed
    public void SetSprinting(bool isSprinting)
    {
        animator.SetBool("IsSprinting", isSprinting);
    }

    //  Teleportation
    public void SetTeleporting(bool isTeleporting)
    {
        animator.SetBool("IsTeleporting", isTeleporting);
    }

    // Shield Dome
    public void SetBlocking(bool isBlocking)
    {
        animator.SetBool("IsBlocking", isBlocking);
    }
}
