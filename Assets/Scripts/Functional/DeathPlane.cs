/* DeathPlane.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * Fail-safe for when an object falls below the level.
 * 
 * 2020-11-21: Added this script.
 * 2020-11-21: Calls SpawnableObjects' Despawn() when triggered.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<SpawnableObject>() != null)
        {
            other.gameObject.GetComponent<SpawnableObject>().Despawn();
        }
    }
}
