/* Points.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * Points behaviour.
 * 
 * 2020-11-22: Added this script.
 * 2020-11-22: Gives player points.
 * 2020-11-22: Added reset.
 * 2020-11-22: Can catch points midair.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : SpawnableObject
{
    private void Awake()
    {
        objType = EnumSpawnObjectType.POINTS;
    }

    public override void Reset()
    {
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<ICharacter>())
        {
            other.gameObject.GetComponentInParent<ICharacter>().UpdateHealth(-Random.Range(10, 20), 0, Vector2.zero);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<ICharacter>())
        {
            other.gameObject.GetComponentInParent<ICharacter>().UpdateHealth(-Random.Range(10, 20), 0, Vector2.zero);
            gameObject.SetActive(false);
        }
    }
}
