/* DetectGround.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-15
 * 
 * A separate trigger collider that detects platforms.  Purpose is to
 * only allow collisions from above the platform so the player can move
 * from below the platform.
 * 
 * 2020-11-15: Added this script.
 * 2020-11-15: Player only collides with platforms when they are colliding from above.
 * 2020-11-15: Issue with moving between touching platforms because OnTriggerEnter2D does not fire.
 *             Changed OnTriggerEnter2D to OnTriggerStay2D to get consistent collision checks.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D cirCollider;

    public bool isOnPlatform;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform") && !cirCollider.isTrigger)
        {
            cirCollider.enabled = true;
            isOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform") && !cirCollider.isTrigger)
        {
            cirCollider.enabled = false;
            isOnPlatform = false;
        }
    }
}
