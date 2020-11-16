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
