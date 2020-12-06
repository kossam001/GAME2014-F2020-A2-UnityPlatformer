/* Attack.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * Responds to a trigger collider that appears when a character attacks.
 * 
 * 2020-11-16: Added this script.
 * 2020-11-16: Added knockback.
 * 2020-11-16: Attack goes on and off.
 * 2020-11-20: Added knockback to the player character.
 * 2020-11-21: Moved damage effects to character
 * 2020-11-22: Fixed a bug where triggers weren't being detected upon enabling them unless they moved on enabling.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Vector2 knockbackForce = new Vector2(100,0);
    public Vector2 direction = new Vector2(1, 1);
    public float maxActiveTime = 0.5f;

    public float activeTime;
    private float activeTimeDelay = 0.1f;
    private bool isAttacking; // To allow a delay before knockback
    private Vector3 OriginalPosition;

    public int pointDamage = 10;
    public int heartDamage = 10;

    public AudioSource audioPlayer;
    public AudioClip hitSound;

    private void Awake()
    {
        OriginalPosition = transform.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        direction.x = transform.parent.transform.localScale.x;

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ICharacter>().UpdateHealth(pointDamage, heartDamage, knockbackForce * direction);
        }
    }

    private void Update()
    {
        activeTime -= Time.deltaTime;
        if (activeTime < 0)
        {
            transform.localPosition = OriginalPosition;
            GetComponent<Collider2D>().enabled = false;
            gameObject.SetActive(false);
        }
    }

    public void attack()
    {
        if (activeTime < 0)
        {
            gameObject.SetActive(true);
            GetComponent<Collider2D>().enabled = true;
            transform.position = transform.position + new Vector3(0.001f, 0); // Colliders need to move a little to trigger
            activeTime = maxActiveTime;
        }
    }
}
