using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public ParticleSystem smokingRunning;
    //private bool canTakeDamege = true;
    public bool isNonLoopAnimation = false;
    public Rigidbody2D rb;
    Vector2 vectorToRight = new Vector2(1,0);
    Vector2 vectorToLeft = new Vector2(-1,0);
    private string currentAnim ="";
    public float moveSpeed =1;
    public float jumpStrength = 1;
    public bool onGround = false;
    
    float movingPlatformVelocityX = 0;

    public SpriteRenderer playerSpriteRenderer;

    public Animator animator;

    public TrailRenderer dashTrailRenderer;
    public float dashPower = 5f;
    Vector2 dashDirection = new Vector2();
    bool isDashing = false;
    bool canDash = true;

    private bool doubleJump = false;


    private bool isWallSliding;
    public float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection ;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f,16f);

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardController();
        WallSlide();
        HandleFallAnimation();
    }

    void KeyboardController()
    {
        if(Input.GetKeyDown(Constant.KEY_DASH)==true)
        {
            if(canDash==true)
            {
                StartCoroutine(PlayerDash());
            }
        }
        bool isMoving = false;
        if (Input.GetKey(Constant.KEY_MOVE_RIGHT) && !isDashing)
        {
                PlayerMoveRight();
            isMoving = true;


        }
        if (Input.GetKey(Constant.KEY_MOVE_LEFT) && !isDashing)
        {         
           
                PlayerMoveLeft();   
            isMoving = true;

        }

        if (isMoving || !onGround)
        {
            if (!smokingRunning.isPlaying)
            {
                smokingRunning.Play();
                
            }
        }
        else
        {
            
            PlayerStopMovement();
            if (smokingRunning.isPlaying)
            {
                smokingRunning.Stop();
            }
        }

        if (Input.GetKeyDown (Constant.KEY_JUMP) == true)
        {

            if (isWallSliding)
            {
                WallJump();
            }
            else
            {
                PlayerJump();
            }
        }
        
    }

    IEnumerator PrepareNonLoopAnimation(string animationName)
    {
        isNonLoopAnimation = true;
        PlayingNoonLoopAnimation(animationName);
        yield return new WaitForEndOfFrame();
        var currentAnimationInfo = animator.GetCurrentAnimatorStateInfo(0);
        if(currentAnimationInfo.IsName(animationName) ==true)
        {
            var animationDuration = currentAnimationInfo.length;
            yield return new WaitForSeconds(animationDuration);
            isNonLoopAnimation=false;
        }
        else
        {
            yield return null;
            isNonLoopAnimation = false;
        }
    }

    private void PlayingAnimation(string animationName)
    {
        
        if (currentAnim != animationName && isNonLoopAnimation == false)
        {
           
            currentAnim = animationName;
            animator.Play(currentAnim);
        }
    }
    private void PlayingNoonLoopAnimation(string animationName)
    {
        if(currentAnim != animationName)
        {            
            currentAnim = animationName;
            animator.Play(currentAnim);
        }
        
    }

    private void PlayerJump()
    {
        
        
        if(onGround==true || doubleJump)
        {
            if (!smokingRunning.isPlaying)
            {
                
                smokingRunning.Play();
            }
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, 1) * jumpStrength, ForceMode2D.Impulse);
            if(onGround == true) StartCoroutine(AnimationJump());
            else StartCoroutine(AnimationDoubleJump());
            doubleJump = !doubleJump;
        }
    }

    IEnumerator AnimationJump()
    {
        yield return new WaitForSeconds(0.1f);
        PlayingAnimation(Constant.ANIM_JUMP);
    }
    IEnumerator AnimationDoubleJump()
    {
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(PrepareNonLoopAnimation(Constant.ANIM_DOUBLE_JUMP));
        PlayingAnimation(Constant.ANIM_JUMP);


    }

    void PlayerRotation(bool boolValue)
    {
        playerSpriteRenderer.flipX = boolValue;
    }

    void PlayerMove(Vector2 move)
    {
        Vector2 newMoveVector = new Vector2((move.x * moveSpeed) + movingPlatformVelocityX, rb.velocity.y);
        rb.velocity = newMoveVector;
        
        if(onGround == true)
        {           
            PlayingAnimation(Constant.ANIM_RUN);
        }
        
    }
    private void PlayerStopMovement()
    {
        if (!Input.GetKey(Constant.KEY_MOVE_LEFT) && !Input.GetKey(Constant.KEY_MOVE_RIGHT))
        {
            rb.velocity = new Vector2(
                Mathf.Lerp(rb.velocity.x, 0, Time.deltaTime * 10f), 
                rb.velocity.y
            );
        }
        AnimotionStop();
    }

    private void AnimotionStop()
    {
       if(onGround == true)
        {
            
            PlayingAnimation(Constant.ANIM_IDLE);
        }
    }

    private void PlayerMoveRight()
    {
        PlayerMove(vectorToRight);
        PlayerRotation(false);
        smokingRunning.Play();

    }

    private void PlayerMoveLeft()
    {
        PlayerMove(vectorToLeft);
        PlayerRotation(true);
        
    }

    IEnumerator PlayerDash()
    {
        dashTrailRenderer.emitting = true;
        canDash = false;
        isDashing = true;
        if(playerSpriteRenderer.flipX == false)
        {
            dashDirection = new Vector2 (1, 0);
        }else
        {
            dashDirection = new Vector2 (-1, 0);
        }
        rb.velocity = dashDirection*dashPower;
        //StartCoroutine(PrepareNonLoopAnimation(Constant.ANIM_DASH));
        yield return new WaitForSeconds(0.1f);
        rb.velocity = dashDirection*moveSpeed;
        isDashing =false;
        dashTrailRenderer.emitting=false;
        yield return new WaitForSeconds(1f);
        canDash = true;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 1.5f, wallLayer);
    }

    private void WallSlide()
    {
        Debug.Log(IsWalled());
        if (IsWalled() && !onGround && rb.velocity.y <0)
        {
            PlayingAnimation(Constant.ANIM_WALL);
            Debug.Log("hihi");
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }


    private void WallJump()
    {
        if(isWallSliding)
        {
            
            isWallSliding = false;
            wallJumpingDirection = playerSpriteRenderer.flipX ? 1 : -1;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            doubleJump = true; 
            
            playerSpriteRenderer.flipX = wallJumpingDirection == -1;

            PlayingAnimation(Constant.ANIM_JUMP);
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
        
        
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }


    private void HandleFallAnimation()
    {
        if (!onGround && !isWallSliding && rb.velocity.y < 0)
        {
            PlayingAnimation(Constant.ANIM_FALL);
        }
    }
}
