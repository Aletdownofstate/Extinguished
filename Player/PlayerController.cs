using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    #region Classes
    [HideInInspector] public Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;    
    
    public Animator animator;
    public ParticleSystem dust;
    public ParticleSystem wallDust;
    public ParticleSystem waterGun;
    #endregion

    #region Variables
    [Header("Debug Settings")]
    public bool controlEnabled;
    public bool dashEnabled;
    public bool doubleJumpEnabled;
    public bool wallJumpEnabled;
    public bool shootEnabled;

    [HideInInspector] public float moveHorizontal;
    private float moveSpeed = 10f;
    public bool isFacingRight = true;
    private bool isMoving;
    
    private float jumpForce = 17.5f;
    private bool isJumping;
    public bool canJump;
    private bool doubleJump;    
    private float coyoteTime = 0.1f;
    private float coyoteTimeCounter;
    private float jBufferTime = 0.2f;
    private float jBufferCounter;

    private bool canDash = true;
    private float dashPower = 24;
    private float dashTime = 0.2f;
    private float dashCooldown = .75f;    
    private bool isDashing;

    private bool isWallSliding;
    private float wallSlideSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpCounter;
    private float wallJumpTime = 0.2f;
    private float wallJumpDuration = 0.4f;
    private Vector2 wallJumpPower = new Vector2(8f, 16f);
    #endregion

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        // Handles the player movement, abilities & jumping

        if (controlEnabled)
        {
            // Movement

            if (isDashing)
            {
                return;
            }

            moveHorizontal = Input.GetAxisRaw("Horizontal");

            if (moveHorizontal == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

            float moveVertical = rb.velocity.y;
            animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
            animator.SetFloat("vertSpeed", moveVertical);

            // Footsteps Sound Effects

            if (rb.velocity.x != 0 && (isGrounded() || isOnPlatform() || canJump) && SoundManager.audioSrc.isPlaying == false && isMoving)
            {
                SoundManager.PlaySound("footsteps");
            }
            else if (rb.velocity.x == 0 && !isJumping && !isMoving)
            {
                SoundManager.audioSrc.Stop();
            }

            // Coyote time

            if (isGrounded() || isOnPlatform())
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            // Jump Buffer

            if (Input.GetButtonDown("Jump"))
            {
                jBufferCounter = jBufferTime;
            }
            else
            {
                jBufferCounter -= Time.deltaTime;
            }

            // Jumping

            if (jBufferCounter > 0f && coyoteTimeCounter > 0f)
            {
                animator.SetBool("isJumping", true);
                isJumping = true;
                canJump = false;
                SoundManager.PlaySound("jump");
                CreateDust();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);                
                jBufferCounter = 0;
            }

            if ((isGrounded() || isOnPlatform()) && !Input.GetButton("Jump"))
            {
                doubleJump = false;
                tr.emitting = false;
            }

            // Jump height based on button press

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);                                
            }

            // Corner jump bug fix - this is probably jank

            if (!isGrounded() && rb.velocity.y == 0)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
                canJump = true;
            }

            if (canJump && Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = true;
                canJump = false;
                animator.SetBool("isJumping", true);
                SoundManager.PlaySound("jump");
                CreateDust();
                jBufferCounter = 0;
            }

            if (canJump && rb.velocity.y < 0)
            {
                canJump = false;
            }

            // Jumping & Falling flags        

            if (rb.velocity.y == 0 && (isGrounded() || isOnPlatform()) || canJump)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
                isJumping = false;
            }

            if (rb.velocity.y < 0)
            {
                animator.SetBool("isFalling", true);
                animator.SetBool("isJumping", false);
            }

            if (!isGrounded() && !isJumping)
            {
                animator.SetBool("isFalling", true);
            }

            if (isOnPlatform() && !isJumping)
            {
                animator.SetBool("isFalling", false);
            }
            
            if (canJump && !isJumping)
            {
                animator.SetBool("isFalling", false);
            }

            // Double jump

            if (doubleJumpEnabled)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    if ((isGrounded() || isOnPlatform()) || isWallJumping || doubleJump)
                    {
                        animator.SetBool("isFalling", false);
                        animator.SetBool("isJumping", true);
                        SoundManager.PlaySound("jump");
                        CreateDust();
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                        doubleJump = !doubleJump;
                        tr.emitting = true;
                    }
                }
            }

            // Dash

            if (dashEnabled)
            {
                if (Input.GetButtonDown("Dash") && canDash)
                {
                    StartCoroutine(Dash());
                }
            }

            // Wallsliding & Wall Jumping            

            if (wallJumpEnabled)
            {
                WallSlide();

                WallJump();
            }

            if (!isWallJumping)
            {
                Flip();
            }
            
            // Shooting

            if (shootEnabled)
            {
                if (Input.GetButton("Fire1"))
                {
                    waterGun.Play();

                    if (isFacingRight)
                    {
                        waterGun.transform.rotation = Quaternion.identity;
                        firePoint.transform.rotation = Quaternion.identity;
                    }
                    else
                    {
                        waterGun.transform.rotation = Quaternion.Euler(0f, 0f, 172f);
                        firePoint.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    }
                }
                else if (!Input.GetButton("Fire1"))
                {
                    waterGun.Stop();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        // Set movement speed

        if (!isWallJumping)
        {
            rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);
            isMoving = true;
        }       
    }

    // Ground Check

    public bool isGrounded()
    {        
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // Platform Check

    public bool isOnPlatform()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.4f, platformLayer);
    }

    // Changes Direction

    private void Flip()
    {
        if (isFacingRight && moveHorizontal < 0f || !isFacingRight && moveHorizontal > 0f)
        {
            if(isGrounded() || isOnPlatform())
            {
                CreateDust();
            }
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;            
        }
    }

    public void SceneTransitionFlip()
    {
        if (!isFacingRight && moveHorizontal == 0f)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // Dashing

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        CreateDust();
        SoundManager.PlaySound("dash");
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        if (isGrounded())
        {
            tr.emitting = false;
        }
        rb.gravityScale = originalGravity;        
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    // Wall Sliding

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && (!isGrounded() || !isOnPlatform()) && moveHorizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            tr.emitting = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    // Wall Jumping

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallDust.Play();
            wallJumpDirection = -transform.localScale.x;
            wallJumpCounter = wallJumpTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpCounter > 0f)
        {
            isWallJumping = true;
            doubleJump = true;
            rb.velocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            CreateDust();
            SoundManager.PlaySound("jump");
            wallJumpCounter = 0f;            

            if (transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpDuration);            
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
        wallDust.Stop();
    }        

    private void CreateDust()
    {
        dust.Play();
    }
}