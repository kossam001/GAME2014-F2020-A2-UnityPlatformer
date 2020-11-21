/* EnemySight.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-16
 * 
 * Logic for enemy sight triggers.
 * 
 * 2020-11-16: Added this script.
 * 2020-11-16: If the enemy sees the player, raise a flag and report distance between enemy and player.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public bool seesPlayer { get; private set; } = false;
    [SerializeField]
    public float distanceToPlayer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            seesPlayer = true;
            distanceToPlayer = Vector2.Distance(collision.gameObject.transform.position, transform.parent.transform.position);
        }
    }
}
