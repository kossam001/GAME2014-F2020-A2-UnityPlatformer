using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
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

    private Rigidbody2D rigidbody2d;

    public bool isGrounded;
    public bool isJumping;
    public float fallVelocityThreshold = 1.0f;
    public float cumulativeJumpForce = 0;
    public float maxJumpForce = 1000;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        armAnimator = arm.GetComponent<Animator>();
        weaponAnimator = weapon.GetComponent<Animator>();
        topAnimator = upperBody.GetComponent<Animator>();
        botAnimator = lowerBody.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (joystick.Horizontal > horizontalSensitivity)
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
        else if (joystick.Horizontal < -horizontalSensitivity)
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

        if ((joystick.Vertical > verticalSensitivity) && cumulativeJumpForce < maxJumpForce)
        {
            // jump
            rigidbody2d.AddForce(Vector2.up * verticalForce);
            cumulativeJumpForce += verticalForce;
            isJumping = true;
        }

        if (isGrounded)
        {
            botAnimator.SetBool("IsGrounded", true);
        }
        else if (!isGrounded)
        {
            botAnimator.SetBool("IsGrounded", false);
        }
    }

    public void Attack()
    {
        armAnimator.SetTrigger("Attack");
        weaponAnimator.SetTrigger("Attack");
        topAnimator.SetTrigger("Attack");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") && rigidbody2d.velocity.y <= fallVelocityThreshold)
        {
            isGrounded = true;
            cumulativeJumpForce = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }
}
