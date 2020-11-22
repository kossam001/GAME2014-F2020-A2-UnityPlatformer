using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int pointDamage = 10;
    public int heartDamage = 10;
    public Vector2 knockbackForce = new Vector2(100, 0);

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<ICharacter>().UpdateHealth(pointDamage, heartDamage, knockbackForce * -Mathf.Sign(other.gameObject.transform.localScale.x));
        }
    }
}
