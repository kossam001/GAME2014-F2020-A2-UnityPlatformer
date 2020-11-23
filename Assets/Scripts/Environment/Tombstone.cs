/* Tombstone.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * Tombstone behaviour.
 * 
 * 2020-11-22: Added this script.
 * 2020-11-22: Gives player points.
 * 2020-11-22: Despawn when hits = 0.
 * 2020-11-22: Added reset.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : SpawnableObject
{
    public int hits = 5;

    private void Awake()
    {
        objType = EnumSpawnObjectType.TOMBSTONE;
    }

    public override void Reset()
    {
        hits = 5;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            hits--;

            if (hits <= 0)
            {
                other.gameObject.GetComponentInParent<ICharacter>().UpdateHealth(-Random.Range(10, 100), 0, Vector2.zero);
                gameObject.SetActive(false);
            }
        }
    }
}
