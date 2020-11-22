/* GroundEnemyController.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-21
 * 
 * AI for land-based enemies.
 * 
 * 2020-11-16: Added this script.
 * 2020-11-16: AI can detect edges of platforms and will not walk off.
 * 2020-11-16: AIs can be knocked back by the player's attack.
 * 2020-11-16: AIs will not freak out in the air.
 * 2020-11-16: AI sees the player and will attack when in range.
 * 2020-11-21: Added Health
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundEnemyController : ICharacter
{
    public Transform lineEndpoint;
    public LayerMask layerMask;

    private Rigidbody2D rigidbody2d;
    public EnemySight enemySight;
    public GameObject attackObject;

    private Attack attackComponent;
    public bool isTherePlatform;
    public float speed;
    public float direction;
    public bool isInAir = false;
    public float attackRange;

    public int soulPoints = 100;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        direction = 1;
        attackComponent = attackObject.GetComponent<Attack>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookForPlatformEdge();
        Move();
        SearchForPlayer();
    }

    private void LookForPlatformEdge()
    {
        isTherePlatform = Physics2D.Linecast(transform.position, lineEndpoint.position, layerMask);

        Debug.DrawLine(transform.position, lineEndpoint.position, Color.green);
    }

    private void Move()
    {
        if (!isInAir)
        {
            if (isTherePlatform)
            {
                rigidbody2d.AddForce(Vector2.right * speed * Time.deltaTime * direction);
            }
            else
            {
                ChangeDirection();
            }

            rigidbody2d.velocity *= 0.90f;
        }
    }

    private void ChangeDirection()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        direction *= -1;
    }

    private void SearchForPlayer()
    {
        if (enemySight.seesPlayer)
        {
            if (enemySight.distanceToPlayer <= attackRange && !attackObject.activeInHierarchy)
            {
                attackComponent.attack();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isInAir = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isInAir = false;
        }
    }

    public override void UpdateHealth(int pointLoss, int heartGain, Vector2 knockbackForce)
    {
        // Turn around when hit from behind
        if (Mathf.Sign(knockbackForce.x) == Mathf.Sign(direction))
        {
            ChangeDirection();
        }

        rigidbody2d.AddForce(knockbackForce);
        soulPoints -= pointLoss;

        if (soulPoints <= 0)
        {
            GameManager.Instance.UpdateHealth(Random.Range(10, 50), 0);
            gameObject.SetActive(false);
        }
    }
}
