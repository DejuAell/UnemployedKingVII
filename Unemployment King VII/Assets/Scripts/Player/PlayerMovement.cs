using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float airControlFactor = 0.5f;  // Air control multiplier to reduce speed in the air

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime; // Time the player can still jump after leaving the ground
    private float coyoteCounter; // Tracks how much time passed since the player left the ground

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX; // Horizontal wall jump force
    [SerializeField] private float wallJumpY; // Vertical wall jump force
    private bool isWallJumping = false;

    [Header("Gravity & Wall Fall")]
    [SerializeField] private float wallGravityScale = 0.5f; // Reduced gravity when on the wall
    [SerializeField] private float normalGravityScale = 7f; // Normal gravity scale
    [SerializeField] private float wallSlideSpeed = 1f; // Speed of falling when on the wall

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    

    private void Awake()
    {
        // Get references for rigidbody, animator, and collider
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when moving left or right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(2, 2, 2);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-2, 2, 2);

        // Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        // Handle jumping input
        if (Input.GetKeyDown(KeyCode.Space)) Jump();

        // Adjust jump height when the jump button is released early
        //if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        //    body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        // Check if the player is on a wall
        if (onWall())
        {
            // If on the wall, set gravity scale to normal gravity (for jumping up)
            if (body.velocity.y > 0) // Jumping up
            {
                body.gravityScale = normalGravityScale;
            }
            else // Sliding down or falling after jump
            {
                body.gravityScale = wallGravityScale;
            }

            // Stop horizontal movement while on the wall
            body.velocity = new Vector2(0, body.velocity.y);

            // Apply wall sliding behavior
            if (body.velocity.y > -wallSlideSpeed)
                body.velocity = new Vector2(body.velocity.x, Mathf.Lerp(body.velocity.y, -wallSlideSpeed, Time.deltaTime * 2f));
        }
        else
        {
            // Restore normal gravity and movement
            body.gravityScale = normalGravityScale;

            // Apply horizontal movement depending on whether player is on the ground or in air
            float finalSpeed = isGrounded() ? speed : speed * airControlFactor;
            body.velocity = new Vector2(horizontalInput * finalSpeed, body.velocity.y);

            // Update coyote time and jump counters
            if (isGrounded())
            {
                coyoteCounter = coyoteTime; // Reset coyote time when grounded
                jumpCounter = extraJumps;   // Reset extra jumps
               // anim.SetBool("Jump", false);

            }
            else
            {
                coyoteCounter -= Time.deltaTime; // Decrease coyote time when in air
            }

            if (!isGrounded())
            {
                anim.SetTrigger("jump");
            }
        }
        

        anim.SetFloat("yVelocity", body.velocity.y);
    }

    private void Jump()
    {
        // If coyote time is available or extra jumps are left, allow the jump
        if (coyoteCounter > 0 || jumpCounter > 0)
        {
            // Handle wall jumping
            if (onWall())
                WallJump();
            else
            {
                if (isGrounded())  // Regular ground jump
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    
                    
                }
                else if (coyoteCounter > 0)  // Jump if coyote time is still valid
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                }
                else if (jumpCounter > 0)  // Extra jumps available
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    jumpCounter--;  // Decrease jump counter
                }
                
            }

            // Reset coyote counter to prevent double jumping
            coyoteCounter = 0;
        }
    }

    private void WallJump()
    {
        // Apply force for the wall jump (horizontal and vertical)
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY), ForceMode2D.Impulse);
        isWallJumping = true;
        wallJumpCooldown = 0;
    }

    private bool isGrounded()
    {
        
        // Use boxcast to check for collision with the ground layer
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    
    }

    private bool onWall()
    {
        // Check for collision with walls
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        // Return true if the player is grounded, not on the wall, and not moving
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
