/* DetectGround.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * A separate trigger collider that detects platforms.  Purpose is to
 * only allow collisions from above the platform so the player can move
 * from below the platform.
 * 
 * 2020-11-15: Added this script.
 * 2020-11-15: Player only collides with platforms when they are colliding from above.
 * 2020-11-15: Issue with moving between touching platforms because OnTriggerEnter2D does not fire.
 *             Changed OnTriggerEnter2D to OnTriggerStay2D to get consistent collision checks.
 * 2020-11-22: Added a ColliderReset so that it refires another collision check before concluding that
 *             there is no platform beneath the character, solving the adjacent platform issue with 
 *             trigger enter.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour
{
    [SerializeField]
    private CircleCollider2D cirCollider;

    public bool isOnPlatform;

    private void OnTriggerEnter2D(Collider2D other)
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

        ColliderReset();
    }

    // Refires trigger
    public void ColliderReset()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;
    }
}
