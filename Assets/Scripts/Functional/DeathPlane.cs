/* DeathPlane.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-23
 * 
 * Fail-safe for when an object falls below the level.
 * 
 * 2020-11-21: Added this script.
 * 2020-11-21: Calls SpawnableObjects' Despawn() when triggered.
 * 2020-11-23: DeathPlane will detect falling platforms.
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
        else if (other.gameObject.CompareTag("Platform"))
        {
            other.gameObject.GetComponent<FallingPlatform>().Reset();
        }
    }
}
