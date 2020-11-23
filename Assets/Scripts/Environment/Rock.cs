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

    private void Awake()
    {
        objType = EnumSpawnObjectType.ROCK;
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
}
