/* Boomerang.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-21
 * 
 * The behaviour for the boomerang.
 * 
 * 2020-11-21: Added this script.
 * 2020-11-21: Moves a certain range before returning to the player.  
 * 2020-11-21: Returns to player on trigger enter.  
 * 2020-11-21: Added knockback.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boomerang : MonoBehaviour
{
    public Transform playerTransform;
    public UnityEvent onPlayerCatch; // Event when the weapon returns to the player

    public float range = 10;
    public float speed = 10;
    public Vector2 knockbackForce = new Vector2(100, 0);
    public int pointDamage = 25;
    public int heartDamage = 10;

    [SerializeField]
    private bool isReturning = false;
    [SerializeField]
    private bool hasReturned = false;
    private Vector3 travelDirection;
    [SerializeField]
    private float distanceTravelled = 0;

    private void Update()
    {
        transform.Rotate(0, 0, transform.eulerAngles.z + 1);
    }

    // Throw in the given direction
    public IEnumerator Throw(Vector3 direction)
    {
        gameObject.SetActive(true);

        travelDirection = direction;
        transform.parent = null; // Unparent from the player so player transform does not affect boomerang motion
        isReturning = false;
        hasReturned = false;

        // Go until the max range is reached
        while (distanceTravelled <= range && !isReturning)
        {
            transform.position += speed * direction * Time.deltaTime;
            distanceTravelled += speed * Time.deltaTime;

            yield return null;
        }

        travelDirection = (playerTransform.position - transform.position).normalized;
        if (!isReturning)
        {
            StartCoroutine(ReturnToPlayer());
        }
    }

    // Using trigger to keep things simple
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player can retrieve the weapon when it is coming back
        if (other.gameObject.CompareTag("Player") && isReturning)
        {
            hasReturned = true;
        }
        
        else if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Functional"))
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<ICharacter>().UpdateHealth(pointDamage, heartDamage, 
                    Vector3.Scale(knockbackForce, new Vector3(Mathf.Sign(travelDirection.x), Mathf.Sign(travelDirection.y))));
            }

            // Allow boomerang to go through solid objects until it reaches max range
            if (distanceTravelled >= range)
            {
                // Recalculate direction when colliding with something other than the player
                travelDirection = (playerTransform.position - transform.position).normalized;
                if (!isReturning)
                {
                    StartCoroutine(ReturnToPlayer());
                }
            }
        }
    }

    private IEnumerator ReturnToPlayer()
    {
        isReturning = true;

        while (!hasReturned)
        {
            transform.position += speed * travelDirection * Time.deltaTime;
            distanceTravelled += speed * Time.deltaTime;

            // Recalculate player direction after travelling a certain range
            if (distanceTravelled >= range)
            {
                travelDirection = (playerTransform.position - transform.position).normalized;
                distanceTravelled = 0;
            }

            yield return null;
        }

        Reset();
    }

    private void Reset()
    {
        gameObject.SetActive(false);
        isReturning = false;
        hasReturned = false;
        transform.parent = playerTransform;
        distanceTravelled = 0;
        transform.localPosition = Vector3.zero;
        onPlayerCatch.Invoke();
    }
}
