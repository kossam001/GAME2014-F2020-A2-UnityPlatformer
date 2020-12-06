/* AirEnemyController.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-28
 * 
 * AI for air-based enemies.
 * 
 * 2020-11-26: Added this script.
 * 2020-11-26: Added attack, stats, movement.
 * 2020-11-28: Added animations.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyController : ICharacter
{
    public EnemySight detection;
    public Projectile projectile;

    public int defaultPoints = 100;
    public int soulPoints = 100;
    public float attackDelay = 0;
    public float maxAttackDelay = 1.0f;

    public float speed = 10;

    private Rigidbody2D rigidbody2d;
    [SerializeField]
    private Animator bodyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        shouldDeactivateOffscreen = false;
        playerTransform = GameManager.Instance.player.transform;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Attack();
    }

    void Move()
    {
        Vector3 playerDirection = playerTransform.position - transform.position;
        if (detection.seesPlayer)
        {
            rigidbody2d.AddForce(-playerDirection.normalized * speed * Time.fixedDeltaTime);
        }
        else
        {
            rigidbody2d.AddForce(playerDirection.normalized * speed * Time.fixedDeltaTime);
        }

        if (playerDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        rigidbody2d.velocity *= 0.90f;
    }

    private void Attack()
    {
        if (!projectile.gameObject.activeInHierarchy)
        {
            audioPlayer.clip = attackSound;
            audioPlayer.Play();

            bodyAnimator.SetTrigger("Attack");
            projectile.Shoot((playerTransform.position - transform.position).normalized);
        }
    }

    public override void Reset()
    {
        soulPoints = defaultPoints;
        gameObject.SetActive(true);
    }

    public override void UpdateHealth(int pointLoss, int heartGain, Vector2 knockbackForce)
    {
        if (pointLoss > 0)
        {
            audioPlayer.clip = hitSound;
            audioPlayer.Play();
        }
        else
        {
            audioPlayer.clip = boostSound;
            audioPlayer.Play();
        }

        rigidbody2d.AddForce(knockbackForce);
        soulPoints -= pointLoss;

        if (soulPoints <= 0)
        {
            GameManager.Instance.UpdateHealth(Random.Range(10, 50), 0);
            Despawn();
        }
    }

}
