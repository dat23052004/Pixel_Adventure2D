using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public ParticleSystem smokeRunning;
    public bool isMoving = false;
    public GameObject bullet;
    public GameObject bouncingBullet;
    public Transform right;
    public Transform left;

    public bool isNonLoopAnaimation = false;
    public PlayerHPBar playerHPBar;

    public Rigidbody2D rb;
    Vector2 vectorToRight = new Vector2(1, 0);
    Vector2 vectorToLeft = new Vector2(-1, 0);
    private string currentAnim = "";
    public float moveSpeed = 1;
    public float walkSpeed = 1;
    public float runSpeed = 1;
    public float jumpStrength = 1;

    public bool onGround = false;
    public SpriteRenderer playerSpriteRenderer;

    public Animator playerAnimator;

    public bool isPressedButtonRight = false;
    public bool isPressedButtonLeft = false;

    public int healthPoint = 1;

    public bool canTakeDamege = true;
    public Color colorShield;
    public Color colorNormal;

    GamepadInput gamepadInput;

    public TrailRenderer dashTrailRenderer;
    public float dashPower = 5f;
    Vector2 dashDirection = new Vector2();
    bool isDashing = false;
    bool canDash = true;
    private void Awake()
    {
        gamepadInput = FindObjectOfType<GamepadInput>();
    }
    private void Start()
    {
        healthPoint = PlayerPrefs.GetInt("PlayerHP", 100);
        playerHPBar.updatePlayerHPBar(healthPoint);
    }

    void Update()
    {
        KeyboardController();
        //touchController();
        //gamepadController();
    }

    void GamepadController()
    {
        if (gamepadInput.LeftAnalogVector2.x <0)
        {
            PlayerMoveLeft();
        }
        else if (gamepadInput.LeftAnalogVector2.x >0)
        {
            PlayerMoveRight();
        }
        else
        {
            PlayerStopMovement();
        }

        if (gamepadInput.onButtonDown["Jump"] == true)
        {
            PlayerJump();
        }
        if (gamepadInput.onButtonHold["Run"])
        {
            PlayerRunOn();
        }
        if (gamepadInput.onButtonUp["Run"] == true)
        {
            PlayerRunOff();
        }
    }
    void TouchController()
    {
        if (isPressedButtonRight == true)
        {
            PlayerMoveRight();
        }
        else if (isPressedButtonLeft == true)
        {
            PlayerMoveLeft();
        }
        else
        {
            PlayerStopMovement();
        }
    }
    void KeyboardController()
    {
        if(Input.GetKeyDown("f") == true)
        {
            if(canDash == true)
            {
                StartCoroutine(PlayerDash());
            }

        }
        if (Input.GetKeyDown("r") == true)
        {
            PlayerAttack();
        }

        if (Input.GetKey("d"))
        {
            if(isDashing == false)
            {
                PlayerMoveRight();
                //isMoving = true;
            }
           
        }
        else if (Input.GetKey("a"))
        {
            if(isDashing == false)
            {
                PlayerMoveLeft();
            }
            //isMoving = true;
        }
        else
        {
            PlayerStopMovement();
            //isMoving = false;
        }

        if (Input.GetKeyDown("space") == true)
        {
            PlayerJump();
            smokeRunning.Stop();
        }


        if (Input.GetKey("left shift") == true)
        {
            PlayerRunOn();
            if ( moveSpeed == runSpeed)
            {
                if (smokeRunning.isPlaying == false)
                {

                    smokeRunning.Play();
                }
            }
            else
            {
               
                smokeRunning.Stop();
            }

        }
        if (Input.GetKeyUp("left shift") == true)
        {
     
            PlayerRunOff();
            smokeRunning.Stop();
        }
    }

    IEnumerator PlayerDash()
    {
        dashTrailRenderer.emitting = true;
        canDash = false;
        isDashing = true;
        if(playerSpriteRenderer.flipX == true)
        {
            dashDirection = new Vector2(1, 0);
        }
        else
        {
            dashDirection = new Vector2 (-1, 0);
        }
        rb.velocity = dashDirection*dashPower;
        StartCoroutine(PrepareNonLoopAnaimation("Player Dash"));
        yield return new WaitForSeconds(0.1f);
        rb.velocity = dashDirection * moveSpeed;
        isDashing = false;
        dashTrailRenderer.emitting = false;
        //end dash
        yield return new WaitForSeconds(1f);
        canDash = true;
    }

    public void PlayerRunOn()
    {
        if (moveSpeed != runSpeed && onGround == true)
        {
            StartCoroutine(ChangePlayerSpeed(runSpeed));
        }
    }
    public void PlayerRunOff()
    {
        if (moveSpeed != walkSpeed)
        {
            StartCoroutine(ChangePlayerSpeed(walkSpeed));
        }
    }

    IEnumerator ChangePlayerSpeed(float newSpeed)
    {
        yield return new WaitUntil(()=> onGround == true);
        moveSpeed = newSpeed;

    }


    void PlayerMove(Vector2 moveVector)
    {
        Vector2 newMoveVector = new Vector2((moveVector.x*moveSpeed)+ movingPlatformVelocityX, rb.velocity.y);
        rb.velocity = newMoveVector;
        if(onGround == true)
        {
            if(moveSpeed == walkSpeed)
            {
                PlayingAnimation("Player Walk");
            }
            if(moveSpeed  == runSpeed)
            {
                PlayingAnimation("Player Run");
            }
        }

    }

    void PlayerRotation(bool boolValue)
    {
        playerSpriteRenderer.flipX = boolValue;
    }

    void AnimotionStop()
    {
        if(onGround == true)
        {
            PlayingAnimation("Player Idle");
        }

    }
    IEnumerator AnimotionJump()
    {
        yield return new WaitForSeconds(0.1f);
        PlayingAnimation("Player Jump");
    }

    void PlayingAnimation(string animation)
    {
        if(currentAnim != animation && isNonLoopAnaimation == false)
        {
            currentAnim = animation;
            playerAnimator.Play(currentAnim);
        }
    }

    void PlayingNonLoopAnimation(string animation)
    {
        if (currentAnim != animation)
        {
            currentAnim = animation;
            playerAnimator.Play(currentAnim);
        }
    }

    IEnumerator PrepareNonLoopAnaimation( string animationName)
    {
        isNonLoopAnaimation = true;
        PlayingNonLoopAnimation(animationName);
        yield return new WaitForEndOfFrame();
        var currentAnimationInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        if(currentAnimationInfo.IsName(animationName) == true)
        {
            var animationDutarion = currentAnimationInfo.length;
            yield return new WaitForSeconds(animationDutarion);
            isNonLoopAnaimation = false;
        }
        else
        {
            yield return null;
            isNonLoopAnaimation = false;
        }   
    }

    void PlayerAttack()
    {
        StartCoroutine(PrepareNonLoopAnaimation("Player Attack"));
        //CreateBullet();
        CreateBouncingBullet();
    }

    void CreateBullet()
    {
        Vector3 bulletPosition = new Vector3();
        Vector2 bulletDirection = new Vector2();
        float bulletSpeed = 3f;
        if(playerSpriteRenderer.flipX == true)
        {
            bulletPosition = right.position;
            bulletDirection = new Vector2(1,0);
        }
        else
        {
            bulletPosition = left.position;
            bulletDirection = new Vector2(-1, 0);

        }

        var newBullet = Instantiate(bullet, bulletPosition,Quaternion.identity,null);
        var newBulletRigitbody = newBullet.GetComponent<Rigidbody2D>();
        newBulletRigitbody.velocity = bulletDirection * bulletSpeed;
    }

    void CreateBouncingBullet()
    {
        Vector3 bulletPosition = new Vector3();
        Vector2 bulletDirection = new Vector2();
        float bulletSpeed = 4f;
        if (playerSpriteRenderer.flipX == true)
        {
            bulletPosition = right.position;
            bulletDirection = new Vector2(1, 1);
        }
        else
        {
            bulletPosition = left.position;
            bulletDirection = new Vector2(-1, 1);

        }

        var newBullet = Instantiate(bouncingBullet, bulletPosition, Quaternion.identity, null);
        var newBulletRigitbody = newBullet.GetComponent<Rigidbody2D>();
        newBulletRigitbody.velocity = bulletDirection * bulletSpeed;
        newBulletRigitbody.AddTorque(-3f);
    }

    public void PlayerMoveRight()
    {
        PlayerMove(vectorToRight);
        PlayerRotation(true);
    }
    public void PlayerMoveLeft()
    {
        PlayerMove(vectorToLeft);
        PlayerRotation(false);
    }

    public void PlayerStopMovement()
    {
        AnimotionStop();
    }

    public void PlayerJump()
    {
        if (onGround == true)
        {
            rb.AddForce(new Vector2(0, 1) * jumpStrength, ForceMode2D.Impulse);
            StartCoroutine(AnimotionJump());
        }
    }

    public void GameRestart()
    {
        string currentSceneName = gameObject.scene.name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void PlayerHealthPointUpdate(int addingValue)
    {
        if(canTakeDamege)
        {
            canTakeDamege = false;
            healthPoint += addingValue;
            playerHPBar.updatePlayerHPBar(healthPoint); // update hp to HPBar
            PlayerPrefs.SetInt("PlayerHP", healthPoint);
            PlayerPrefs.Save();

            StartCoroutine(GivePlayerShield());
        }

    }

    IEnumerator GivePlayerShield()
    {
        playerSpriteRenderer.color = colorShield;
        yield return new WaitForSeconds(3);
        canTakeDamege = true;
        playerSpriteRenderer.color = colorNormal;
    }

    float movingPlatformVelocityX = 0;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "MovingPlatform"))
        {
            var movingPlatform = collision.gameObject.GetComponent<Rigidbody2D>();
            movingPlatformVelocityX = movingPlatform.velocity.x;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "MovingPlatform"))
        {
            movingPlatformVelocityX = 0;
        }
    }
}

