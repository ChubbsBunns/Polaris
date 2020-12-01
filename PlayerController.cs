using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour

{
    [Header("Movement")]
    public float acceleration;
    public float timeFromZeroToMaxHorizontalSpeed = 0.3f;
    public float maxHorizontalSpeed;
    public float timeFromMaxHorizontalSpeeedToZero = 0.2f;
    public float deceleration;
    public float currentSpeed;
    float HorizontalInput;
    public bool facingRight;
    private Rigidbody2D rb;

    public float acceleratorMovementSpeedBoost;
    public bool imOnAHorizontalRightAccelerator;
    public bool imOnAHorizontalLeftAccelerator;
    public bool imOnAVerticalUpAccelerator;
    public bool imOnAVerticalDownAccelerator;

    [Header("Last Directional Inputs")]
    public float lastHorizontalInput;
    public float lastVerticalInput;
    public float lastDirectionalInput;
    // lastDirectionalInput reference: 
    // left = 1
    // right = 2
    // up = 3
    // down = 4

    [Header("Jumping")]
    public float jumpSpeed = 10f;
    public int jumpThreshold = 10;
    public int stepsJumped = 0;
    public bool isGrounded = false;
    public Transform groundCheckMiddle;
    public Transform groundCheckFront;
    public Transform groundCheckBack;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float stopJumpFastSpeed;
    public bool ableToJump;
    public bool dontContiuouslyJump;
    public int jumpCount;
    public bool cantJumpAnymore;
    public int maxNumberOfJumpsPermitted;

    public float firstJumpCheck;
    public float currentJumpDistanceFromGround;
    public float jumpCountPlaceholder = 1;

    [Header("Falling")]
    public float maximumFloatingFallSpeed;

    [Header("Dashing")]

    public float dashForce;
    public float dashForceX;
    public float dashForceY;
    public float doubleTapTime;
    public float VerticalInput;
    public float dashDuration;
    public float timeAllowanceForInputDashing;
    public float dashHorizontalInput;
    public float dashVerticalInput;
    public float dashDirection;
    public float nextDashTime;
    public float timeBetweenDashes;

    public float timeCheck;

    [Header("Health")]
    public int playerHealth;

    [Header("Physics")]
    public float originalGravityScale;
    public float fallingSlowerGravityScale;
    public float fallingFasterGravityScale;
    public float currentGravityScale;


    [Header("WallSliding And WallJumping")]
    public bool isTouchingFront;
    public Transform frontCheck;
    public Transform frontCheck2;
    public bool wallSliding;
    public float wallSlidingSpeed;
    public float wallCheckRadius;

    public bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    [Header("Player States")]
    public bool isJumping;
    public bool isDashing;
    public bool playerIsControllable;
    public bool isAirBorne;

    [Header("Moves Unlocked")]
    public bool dashUnlocked ;
    public bool doubleJumpUnlocked;
    public bool wallSlideAndJumpUnlocked;

    [Header("Miscellaneous")]
    public SceneLoader[] scenePortals;
    public CinemachineVirtualCamera cinemachineCamera;
    public bool actionButton;
    public GameDataLog gameDataLog;

    private void Awake()
    {
        scenePortals = FindObjectsOfType<SceneLoader>();
        foreach (SceneLoader Portal in scenePortals)
        {
            Portal.FindMyObjects();
        }

        gameDataLog = FindObjectOfType<GameDataLog>();


    }

    void Start()
    {
        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachineCamera.Follow = gameObject.transform;
        if (FindObjectsOfType<PlayerController>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        acceleration = maxHorizontalSpeed / timeFromZeroToMaxHorizontalSpeed;
        deceleration = maxHorizontalSpeed / timeFromMaxHorizontalSpeeedToZero;
        if (gameDataLog.Log_wallStuffUnlocked )
        {
            wallSlideAndJumpUnlocked = true;
        }

        if (gameDataLog.log_dashIsUnlocked)
        {
            dashUnlocked = true;
        }

        if (gameDataLog.Log_doubleJumpIsUnlocked)
        {
            maxNumberOfJumpsPermitted = 2;
        }
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        timeCheck = Time.time;
        jumpCountPlaceholder = Mathf.Clamp(jumpCountPlaceholder, 1, maxNumberOfJumpsPermitted);

        MovingHorizontally();
        GroundedCheck();
        DoubleJumpHandler();
        JumpInputs();

        GetVerticalInputs();

        if (dashUnlocked)
        {
            DoIDash();
        }

        ImOnAnAccelerator();

        AmIAirborne();
        FallingSpeedCheck();
        

        if (wallSlideAndJumpUnlocked)
        {
            WallSlideCheck();
            WallJump();
            WallJumpCheck();
            WallSlideJumpCountHandler();
        }

        ActionButtonDown();
  
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
            Debug.Log("ive quit");
        }
    }



    private void FixedUpdate()
    {
        DoIFlip();
        Jump();
        DashCheck();
    }

    public void ActionButtonDown()
    {
        if (Input.GetButtonDown("Action"))
        {
            actionButton = true;
        }

        if (Input.GetButtonUp("Action"))
        {
            actionButton = false;
        }
       
    }
    private void MovingHorizontally()
    {
        // getting horizontal input and applying acceleration and deceleration
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        if (Mathf.Abs(HorizontalInput) > 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }


        if (HorizontalInput == 0)
        {
            currentSpeed -= deceleration * Time.deltaTime;
        }

        //remembering last input 1 or -1
        if (HorizontalInput == 1)
        {
            lastHorizontalInput = 1;
            lastDirectionalInput = 2;
        }
        else if (HorizontalInput == -1)
        {
            lastHorizontalInput = -1;
            lastDirectionalInput = 1;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxHorizontalSpeed);
        if (rb.velocity.x > maxHorizontalSpeed)
        {
            currentSpeed = maxHorizontalSpeed;
        }

        // the actual velocity code
        rb.velocity = new Vector2(currentSpeed * lastHorizontalInput , rb.velocity.y);
    }

    private void DoIDash()
    {
        if (Input.GetButtonDown("Dash") && Time.time > nextDashTime)
        {
            if ((lastDirectionalInput == 1f ))
            {
                StartCoroutine(Dash(-1f));
            }
            else if (lastDirectionalInput == 2 && Time.time > nextDashTime)
            {
                StartCoroutine(Dash(1f));
            }
        }
    }

    private void GetVerticalInputs()
    {
        VerticalInput = Input.GetAxisRaw("Vertical");
        if (VerticalInput == 1)
        {
            lastVerticalInput = 1;
            lastDirectionalInput = 3;
        }
        else if (VerticalInput == -1)
        {
            lastHorizontalInput = -1;
            lastDirectionalInput = 4;
        }
    }


    void GroundedCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckMiddle.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheckMiddle.position, checkRadius, whatIsGround) || Physics2D.OverlapCircle(groundCheckBack.position, checkRadius, whatIsGround);
        if (isGrounded)
        {
            ableToJump = true;
        }
    }
    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
        currentSpeed = 0;
    }

    void DoIFlip()
    {
        if (HorizontalInput > 0 && facingRight == false)
        {
            Flip();
        }

        if (HorizontalInput < 0 && facingRight == true)
        {
            Flip();
        }
    }

    //jumping
    void JumpInputs()
    {
        if (dontContiuouslyJump == false)
        {
            if (Input.GetButton("Jump") && cantJumpAnymore == false)
            {
                isJumping = true;
            }

            if (!Input.GetButton("Jump") && stepsJumped < jumpThreshold && isJumping)
            {
                StopJumpQuick();
            }
            else if (!Input.GetButton("Jump") && stepsJumped > jumpThreshold && isJumping)
            {
                StopJumpSlow();
            }
        }
        HandlingJumpStates();
    }

    void HandlingJumpStates()
    {

        if (Input.GetButton("Jump"))
        {
            dontContiuouslyJump = true;
        }
        else
        {
            dontContiuouslyJump = false;
        }
    }
    void Jump()
    {
        if (isJumping == true)
        {
            if (stepsJumped < jumpThreshold)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                stepsJumped++;
            }
            else
            {
                StopJumpSlow();
            }
        }
    }

    void StopJumpQuick()
    {
        //stops the player from jumping immediately, causing them to fall down as soon as the button is released ( is used when the player stops his jump prematurely)
        stepsJumped = 0;
        rb.velocity = new Vector2(rb.velocity.x, stopJumpFastSpeed);
        isJumping = false;
    }

    void StopJumpSlow()
    {
        //stops the jump but lets the player hang in the air for awhile. (when the player maxes out his or her jump time)
        stepsJumped = 0;
        isJumping = false;
    }

    void DoubleJumpHandler()
    {
        if (isGrounded)
        {
            cantJumpAnymore = false;
            jumpCount = 0;
            jumpCountPlaceholder = 1;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpCountPlaceholder += 1;
            if (jumpCount == 0)
            {
                jumpCountPlaceholder = 1;
            }

            jumpCount += 1;
        }

        if (jumpCount > maxNumberOfJumpsPermitted)
        {
            cantJumpAnymore = true;
        }

    }


    //check for isfalling
    private void AmIAirborne()
    {
        if (isGrounded == false)
        {
            isAirBorne = true;
        }
        else
        {
            isAirBorne = false;
        }

        if (isAirBorne == true)
        {
            jumpCount = (int)jumpCountPlaceholder;

        }
    }

    private void WallSlideJumpCountHandler()
    {
        if (wallSliding == true)
        {
            jumpCount = 0;
        }
    }


    //falling speed check uses gravity to manipulate fall speed. future manipulations of gravity need to take note of this
    private void FallingSpeedCheck()
    {

        if (isAirBorne == true && Input.GetButton("Up"))
        {
            currentGravityScale = fallingSlowerGravityScale;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, maximumFloatingFallSpeed, 100000));
        }
        else if (isAirBorne == true && Input.GetButton("Down"))
        {
            currentGravityScale = fallingFasterGravityScale;
        }
        else
        {
            currentGravityScale = originalGravityScale;
        }
        rb.gravityScale = currentGravityScale;
    }

    private void WallSlideCheck()
    {
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, wallCheckRadius, whatIsGround) || Physics2D.OverlapCircle(frontCheck2.position, wallCheckRadius, whatIsGround);
        if (isTouchingFront == true && isGrounded == false && HorizontalInput != 0)
        {
            wallSliding = true;
        }

        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
        }
    }

    void WallJumpCheck()
    {
        if (wallJumping == true)
        {
            if (facingRight)
            {
                rb.velocity = new Vector2(xWallForce, yWallForce);
            }
            else
            {
                rb.velocity = new Vector2(-xWallForce, yWallForce);
            }
        }
    }

    private void WallJump()
    {
        if (Input.GetButtonDown("Jump") && wallSliding && HorizontalInput != 0)
        {
            wallSliding = false;
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheckMiddle.position, checkRadius);
        Gizmos.DrawSphere(groundCheckFront.position, checkRadius);
        Gizmos.DrawSphere(groundCheckBack.position, checkRadius);
        Gizmos.DrawSphere(frontCheck.position, wallCheckRadius);
        Gizmos.DrawSphere(frontCheck2.position, wallCheckRadius);
    }

    IEnumerator Dash(float direction)
    {
        isDashing = true;
        if ((lastDirectionalInput == 1 || lastDirectionalInput == 2) && isDashing)
        {
            dashDirection = direction;
            nextDashTime = Time.time + timeBetweenDashes;
        }
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    private void DashCheck()
    {
        if (isDashing)
        {
            rb.velocity = new Vector2(dashForceX * dashDirection + rb.velocity.x, rb.velocity.y);
        }
       else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }

    //sets the player to the location of a portal when the scene loads
    public void InstantiateMeHere(Transform myNewPosition)
    {
        transform.position = myNewPosition.position;
    }

    public void ImOnAnAccelerator()
    {
        if (imOnAHorizontalRightAccelerator == true)
        {
            rb.velocity = new Vector2(rb.velocity.x + acceleratorMovementSpeedBoost, rb.velocity.y);
        }

        else if (imOnAHorizontalLeftAccelerator == true)
        {
            rb.velocity = new Vector2(rb.velocity.x - acceleratorMovementSpeedBoost, rb.velocity.y);
        }

        else if (imOnAVerticalUpAccelerator == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + acceleratorMovementSpeedBoost);
        }

        else if (imOnAVerticalDownAccelerator == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - acceleratorMovementSpeedBoost);
        }

        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }



}

