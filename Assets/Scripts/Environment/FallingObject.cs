/* FallingObject.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * As a part of environment hazards, object will fall from above.
 * Some objects will land on platforms while others will pass through.
 * 
 * 2020-11-21: Added this script.
 * 2020-11-22: Can do damage.
 * 2020-11-22: Added a ColliderReset to refire onCollisionEnter when it no longer is a trigger.
 * 2020-11-22: Adding to respawn code.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public int fallThroughAmount;
    public bool shouldStopNearPlayer = false; // Falling object should stop falling when near player's elevation
    public Transform playerTransform;
    public float elevationThreshold = 3;
    public bool shouldSlowFall = true;
    public bool shouldDealDamage = false;

    public Collider2D fallingTrigger;
    private Rigidbody2D rigidbody2d;

    public int pointDamage = 10;
    public int heartDamage = 10;
    public Vector2 knockbackForce = new Vector2(100, 0);

    private void Start()
    {
        fallingTrigger = GetComponent<Collider2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        if (shouldSlowFall)
        {
            rigidbody2d.velocity *= 0.90f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!enabled)
        {
            return;
        }

        if (other.gameObject.CompareTag("Platform") && fallingTrigger.enabled)
        {
            fallThroughAmount--;

            if (fallThroughAmount <= 0)
            {
                fallingTrigger.isTrigger = false;
                ColliderReset();

                this.enabled = false;
                
            }
            else if (shouldStopNearPlayer && Mathf.Abs(playerTransform.position.y - transform.position.y) <= elevationThreshold)
            {
                fallThroughAmount = 0;
                fallingTrigger.isTrigger = false;

                ColliderReset();

                this.enabled = false;
            }
        }

        if (shouldDealDamage)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<ICharacter>().UpdateHealth(pointDamage, heartDamage, knockbackForce * -Mathf.Sign(other.gameObject.transform.localScale.x));
            }
        }
    }

    // Refires trigger
    public void ColliderReset()
    {
        fallingTrigger.enabled = false;
        fallingTrigger.enabled = true;
    }

    // Restarts falling
    public void StartFalling(int fallAmount, bool slowFall, bool stopFall, bool doDamage, Vector3 startPosition)
    {
        fallingTrigger.isTrigger = true;
        fallThroughAmount = fallAmount;
        shouldSlowFall = slowFall;
        shouldStopNearPlayer = stopFall;
        shouldDealDamage = doDamage;

        GetComponent<SpawnableObject>().Reset();
        gameObject.transform.position = startPosition;
        this.enabled = true;
    }
}
