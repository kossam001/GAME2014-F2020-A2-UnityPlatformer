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
        if (other.gameObject.tag == "Platform")
        {
            Debug.Log("DAJDJIOJ");
            cirCollider.enabled = true;
            isOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("DAJDJIOJ");
        if (other.gameObject.tag == "Platform")
        {
            Debug.Log("DAJDJIOJ");
            cirCollider.enabled = false;
            isOnPlatform = false;
        }
    }
}
