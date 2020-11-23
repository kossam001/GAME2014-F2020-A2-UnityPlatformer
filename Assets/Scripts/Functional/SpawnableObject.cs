/* SpawnableObject.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * Used to generalize all non-static elements in the game and have them share common functionality.
 * 
 * 2020-11-22: Added this script.
 * 2020-11-22: All dynamic objects should be resetable.
 * 2020-11-22: Spawnable objects should despawn when outside of player's activity zone
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    public abstract void Reset();
    public Transform playerTransform;
    public bool shouldDeactivateOffscreen = true;

    private void Start()
    {
        playerTransform = SpawnManager.Instance.playerTransform;
    }

    private void Update()
    {
        if (playerTransform.position.y - transform.position.y > 20 && shouldDeactivateOffscreen)
        {
            GetComponent<SpawnableObject>().Despawn();
        }
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
        SpawnManager.Instance.RetrieveDead(gameObject);
    }
}
