/* Rock.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * Rock behaviour.
 * 
 * 2020-11-21: Added this script.
 * 2020-11-22: Moved damage to falling object.
 * 2020-11-22: Added reset.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : SpawnableObject
{
    private void Awake()
    {
        objType = EnumSpawnObjectType.ROCK;
    }

    public override void Reset()
    {
        transform.localScale = new Vector3(Random.Range(1, 3), Random.Range(1, 3));
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
    }
}
