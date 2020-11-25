/* SpawnableObject.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-24
 * 
 * Used to generalize all non-static elements in the game and have them share common functionality.
 * 
 * 2020-11-22: Added this script.
 * 2020-11-22: All dynamic objects should be resetable.
 * 2020-11-22: Spawnable objects should despawn when outside of player's activity zone.
 * 2020-11-22: Spawns different types of falling objects.
 * 2020-11-24: Changed Despawn to virtual so it can be overwritten in PlayerController.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    public abstract void Reset();
    public Transform playerTransform;
    public bool shouldDeactivateOffscreen = true;

    public EnumSpawnObjectType objType;

    private void Update()
    {
        if (playerTransform != null && playerTransform.position.y - transform.position.y > 20 && shouldDeactivateOffscreen)
        {
            GetComponent<SpawnableObject>().Despawn();
        }
        else
        {
            if (playerTransform == null)
            {
                playerTransform = GameManager.Instance.GetPlayerTransform();
            }
        }
    }

    public virtual void Despawn()
    {
        gameObject.SetActive(false);
        SpawnManager.Instance.RetrieveDead(gameObject);
    }
}
