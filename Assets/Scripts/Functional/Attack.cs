using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Vector2 knockbackForce = new Vector2(500,500);
    public Vector2 direction = new Vector2(1, 1);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        direction.x = transform.parent.transform.localScale.x;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce * direction);
        }
    }
}
