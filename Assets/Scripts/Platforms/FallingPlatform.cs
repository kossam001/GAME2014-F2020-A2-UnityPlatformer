/* FallingPlatform.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-23
 * 
 * Platform will fall down the level when something lands on it.  
 * Has physics effects.
 * 
 * 2020-11-23: Added this script.  Falling platform will knock objects down and deal damage.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallTimer = 3;
    public float fallSpeed = 5;
    public int pointDamage = 10;
    public int heartDamage = 10;
    public bool isAlwaysFalling = false;

    private bool isCollapsing = false;
    private bool isFalling = false;
    private Rigidbody2D rigidbody2d;
    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isAlwaysFalling)
        {
            isFalling = true;
        }

        if (isFalling)
        {
            rigidbody2d.MovePosition(rigidbody2d.position + new Vector2(0,-1) * fallSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isCollapsing)
        {
            StartCoroutine(StartFalling());
        }
        if (isFalling)
        {
            // No damage if player jumps through the plaform as it falls or player is colliding from above.
            if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) &&
                other.GetContact(0).normal.y > 0 &&
                !other.collider.isTrigger)
            {
                other.gameObject.GetComponent<ICharacter>().UpdateHealth(pointDamage, heartDamage, Vector3.zero);
            }
        }
    }

    IEnumerator StartFalling()
    {
        isCollapsing = true;
        yield return new WaitForSeconds(fallTimer);
        isFalling = true;
    }

    public void Reset()
    {
        isCollapsing = false;
        isFalling = false;
        transform.position = startingPosition;
    }
}
