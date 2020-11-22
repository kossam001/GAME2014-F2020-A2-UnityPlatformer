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
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<ICharacter>())
        {
            other.gameObject.GetComponentInParent<ICharacter>().UpdateHealth(-Random.Range(10, 20), 0, Vector2.zero);
            gameObject.SetActive(false);
        }
    }
}
