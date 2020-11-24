/* Rock.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-23
 * 
 * Rock behaviour.
 * 
 * 2020-11-21: Added this script.
 * 2020-11-22: Moved damage to falling object.
 * 2020-11-22: Added reset.
 * 2020-11-23: Rocks are obstacles now.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : SpawnableObject
{
    public int hits = 5;
    public int pointDamage = 10;
    public int heartDamage = 10;

    [SerializeField]
    private bool isFalling = false;
    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        objType = EnumSpawnObjectType.ROCK;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rigidbody2d.velocity.magnitude > 1)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    public override void Reset()
    {
        transform.localScale = new Vector3(Random.Range(1, 3), Random.Range(1, 3));
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            hits--;

            if (hits <= 0)
            {
                Despawn();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
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
}
