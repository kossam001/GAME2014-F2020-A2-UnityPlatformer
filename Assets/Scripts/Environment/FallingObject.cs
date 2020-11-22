/* FallingObject.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-21
 * 
 * As a part of environment hazards, object will fall from above.
 * Some objects will land on platforms while others will pass through.
 * 
 * 2020-11-21: Added this script.
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

    public Collider2D fallingTrigger;
    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
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
                this.enabled = false;
                
            }
            else if (shouldStopNearPlayer && Mathf.Abs(playerTransform.position.y - transform.position.y) <= elevationThreshold)
            {
                fallingTrigger.isTrigger = false;
                this.enabled = false;
                fallThroughAmount = 0;
            }
        }
    }

    // Restarts falling
    public void StartFalling(int fallAmount, bool slowFall, bool stopFall)
    {
        fallingTrigger.isTrigger = true;
        fallThroughAmount = fallAmount;
        shouldSlowFall = slowFall;
        shouldStopNearPlayer = stopFall;

        this.enabled = true;
    }
}
