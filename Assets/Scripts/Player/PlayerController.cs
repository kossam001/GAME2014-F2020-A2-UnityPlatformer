/* PlayerController.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-30
 * 
 * Allows player to control the character.
 * 
 * 2020-11-15: Added this script.
 * 2020-11-15: Added movement and jumping.
 * 2020-11-15: Fixed an issue where the player can hold jump and continually platform hop.
 *             Player jump only allowed when they are falling.
 * 2020-11-16: Added attacking and variable jump heights.
 * 2020-11-21: Added health.
 * 2020-11-21: Moved health to game manager.
 * 2020-11-21: Added fall damage.
 * 2020-11-22: Syncing attack animation with attack collider.
 * 2020-11-22: Added reset.
 * 2020-11-23: Adding events to player actions.
 * 2020-11-26: Holding down on the joystick brings up skill menu.
 * 2020-11-30: Added player death.
 * 2020-12-03: Adjusting horizontal movement speed.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class PlayerController : ICharacter
{
    public Joystick joystick;
    public Attack attack;
    public float horizontalSensitivity;
    public float horizontalForce;
    public float verticalSensitivity;
    public float verticalForce;

    [SerializeField]
    private GameObject arm;
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private GameObject upperBody;
    [SerializeField]
    private GameObject lowerBody;

    private Animator armAnimator;
    private Animator weaponAnimator;
    private Animator topAnimator;
    private Animator botAnimator;

    public Rigidbody2D rigidbody2d;

    public bool isGrounded;
    public bool isFalling;
    public float fallVelocityThreshold = 1.0f;
    public float cumulativeJumpForce = 0;
    public float maxJumpForce = 1000;
    public float maxHorizontalVelocity = 10;
    public float totalFallDistance = 0;

    // Events for passive skill use.
    public UnityEvent onAttack; 
    public UnityEvent onJump;
    public UnityEvent onMove;

    public GameObject skillPanel;
    public GameObject erasePanel;

    public AudioClip jumpSound;

    public CinemachineVirtualCamera vcam1;
    public CinemachineBasicMultiChannelPerlin perlin;
    public float maxShakeTime;
    public float shakeTimer;
    public float shakeIntensity;
    public bool isCameraShaking;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.player = gameObject;
        GameManager.Instance.ResetPlayer();

        objType = EnumSpawnObjectType.PLAYER;

        playerTransform = transform;
        rigidbody2d = GetComponent<Rigidbody2D>();

        armAnimator = arm.GetComponent<Animator>();
        weaponAnimator = weapon.GetComponent<Animator>();
        topAnimator = upperBody.GetComponent<Animator>();
        botAnimator = lowerBody.GetComponent<Animator>();

        audioPlayer = GetComponent<AudioSource>();

        maxShakeTime = 0.3f;
        shakeTimer = maxShakeTime;

        StartCoroutine(FindGameManager());
    }

    private void Start()
    {
        // So the stat labels display the initial value
        GameManager.Instance.UpdateHealth(0, 0);

        vcam1 = FindObjectOfType<CinemachineVirtualCamera>();
        perlin = vcam1.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    IEnumerator FindGameManager()
    {
        while(GameManager.Instance == null)
        {
            yield return null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
        //DisplaySkills();

        if (isCameraShaking)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0.0f)
            {
                perlin.m_AmplitudeGain = 0.0f;
                isCameraShaking = false;
                shakeTimer = maxShakeTime;
            }
        }
    }

    private void Move()
    {
        if (joystick.Horizontal > horizontalSensitivity && rigidbody2d.velocity.x < maxHorizontalVelocity)
        {
            // move right
            rigidbody2d.AddForce(Vector2.right * horizontalForce * Time.deltaTime);

            if (transform.localScale.x < 0)
                transform.localScale = transform.localScale * new Vector2(-1, 1);

            armAnimator.SetInteger("AnimState", (int)PlayerMovementState.RUN);
            weaponAnimator.SetInteger("AnimState", (int)PlayerMovementState.RUN);
            topAnimator.SetInteger("AnimState", (int)PlayerMovementState.RUN);
            botAnimator.SetInteger("AnimState", (int)PlayerMovementState.RUN);
        }
        else if (joystick.Horizontal < -horizontalSensitivity && rigidbody2d.velocity.x > -maxHorizontalVelocity)
        {
            // move left
            rigidbody2d.AddForce(Vector2.left * horizontalForce * Time.deltaTime);

            if (transform.localScale.x > 0)
                transform.localScale = transform.localScale * new Vector2(-1, 1);

            armAnimator.SetInteger("AnimState", (int)PlayerMovementState.RUN);
            weaponAnimator.SetInteger("AnimState", (int)PlayerMovementState.RUN);
            topAnimator.SetInteger("AnimState", (int)PlayerMovementState.RUN);
            botAnimator.SetInteger("AnimState", (int)PlayerMovementState.RUN);
        }
        else
        {
            armAnimator.SetInteger("AnimState", (int)PlayerMovementState.IDLE);
            weaponAnimator.SetInteger("AnimState", (int)PlayerMovementState.IDLE);
            topAnimator.SetInteger("AnimState", (int)PlayerMovementState.IDLE);
            botAnimator.SetInteger("AnimState", (int)PlayerMovementState.IDLE);
        }
    }

    public void DisplaySkills()
    {
        if (joystick.Vertical < -verticalSensitivity * 1.1f)
        {
            skillPanel.GetComponent<Canvas>().enabled = true;
            erasePanel.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            skillPanel.GetComponent<Canvas>().enabled = false;
            erasePanel.GetComponent<Canvas>().enabled = false;
        }
    }

    public void Jump()
    {
        onJump.Invoke();

        // Should not be possible to jump again when falling
        if ((joystick.Vertical > verticalSensitivity) && cumulativeJumpForce < maxJumpForce && !isFalling)
        {
            // Play sound once at the beginning of a jump
            if (isGrounded)
            {
                audioPlayer.clip = jumpSound;
                audioPlayer.Play();
            }

            // jump
            rigidbody2d.AddForce(Vector2.up * verticalForce);
            cumulativeJumpForce += verticalForce;
        }
        // Character should start falling when the joystick is released
        else if (joystick.Vertical < verticalSensitivity && !isGrounded)
        {
            isFalling = true;
        }

        if (isGrounded)
        {
            botAnimator.SetBool("IsGrounded", true);
        }
        else if (!isGrounded)
        {
            botAnimator.SetBool("IsGrounded", false);
        }

        // Not checking when max force reached because there is additional upward velocity after reaching max jump force
        // Point where character starts falling
        // Figure out fall distance
        if (rigidbody2d.velocity.y <= fallVelocityThreshold && !isGrounded)
        {
            totalFallDistance += rigidbody2d.velocity.y;
        }
    }

    public void Attack()
    {
        if (attack.activeTime < 0)
        {
            audioPlayer.clip = attackSound;
            audioPlayer.Play();

            attack.attack();
            armAnimator.SetTrigger("Attack");
            weaponAnimator.SetTrigger("Attack");
            topAnimator.SetTrigger("Attack");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // The velocity check is to make sure that the player can't jump off a platform on the way up
        if (other.gameObject.CompareTag("Platform") && rigidbody2d.velocity.y <= fallVelocityThreshold)
        {
            isGrounded = true;
            isFalling = false;
            cumulativeJumpForce = 0;

            // Fall damage
            int fallDamage = -(int)Mathf.Min(maxJumpForce * 1.5f / rigidbody2d.gravityScale + totalFallDistance, 0);
            // Stop sound effects from continously playing
            if (fallDamage > 0)
            {
                UpdateHealth(fallDamage, 0, Vector2.zero);
            }

            totalFallDistance = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
        ColliderReset();
    }

    // Refires trigger
    public void ColliderReset()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;
    }

    public override void UpdateHealth(int pointLoss, int heartGain, Vector2 knockbackForce)
    {
        if (pointLoss > 0)
        {
            ShakeCamera();
            audioPlayer.clip = hitSound;
            audioPlayer.Play();
        }
        else
        {
            audioPlayer.clip = boostSound;
            audioPlayer.Play();
        }

        rigidbody2d.AddForce(knockbackForce * (5 * ((GameManager.Instance.UpdateHealth(-pointLoss, heartGain) - heartGain) / 100)));

        if (GameManager.Instance.playerScore <= 0)
        {
            GameManager.Instance.PlayerGameOver();
        }
    }

    public override void Despawn()
    {
        Reset();
        // Falling into the death plane does not reset this
        totalFallDistance = 0;
    }

    public override void Reset()
    {
        GameManager.Instance.PlayerGameOver();
    }

    private void ShakeCamera()
    {
        perlin.m_AmplitudeGain = shakeIntensity;
        isCameraShaking = true;
    }
}
