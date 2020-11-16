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
        if (other.gameObject.CompareTag("Platform"))
        {
            cirCollider.enabled = true;
            isOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            cirCollider.enabled = false;
            isOnPlatform = false;
        }
    }
}
