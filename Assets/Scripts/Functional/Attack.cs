/* Attack.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-20
 * 
 * Responds to a trigger collider that appears when a character attacks.
 * 
 * 2020-11-16: Added this script.
 * 2020-11-16: Added knockback.
 * 2020-11-16: Attack goes on and off.
 * 2020-11-20: Added knockback to the player character.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Vector2 knockbackForce = new Vector2(100,0);
    public Vector2 direction = new Vector2(1, 1);
    public float maxActiveTime = 0.5f;

    private float activeTime;
    private float activeTimeDelay = 0.1f;
    private bool isAttacking; // To allow a delay before knockback

    private void OnTriggerEnter2D(Collider2D collision)
    {
        direction.x = transform.parent.transform.localScale.x;

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(knockbackForce * direction);
        }
    }

    private void Update()
    {
        activeTime -= Time.deltaTime;
        if (activeTime < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void attack()
    {
        gameObject.SetActive(true);
        activeTime = maxActiveTime;
    }
}
