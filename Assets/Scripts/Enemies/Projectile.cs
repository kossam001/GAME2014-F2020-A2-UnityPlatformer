/* Projectile.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-26
 * 
 * Projectile attack for enemies.
 * 
 * 2020-11-26: Added this script.
 * 2020-11-26: Set the rotation of the attack.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    public Vector2 knockbackForce = new Vector2(100, 0);
    public int pointDamage = 10;
    public int heartDamage = 10;
    public int speed = 100;
    public float maxDistance;
    
    public Transform parent;
    public float distance;

    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        parent = transform.parent;
        rigidbody2d = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + direction * speed * Time.deltaTime);
        distance += speed * Time.deltaTime;

        Reset();
    }

    private void Reset()
    {
        if (distance > maxDistance)
        {
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            distance = 0;
            gameObject.SetActive(false);
        }
    }

    public void Shoot(Vector3 _direction)
    {
        direction = _direction;
        transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x) + 25, Vector3.forward);
        transform.SetParent(null);

        // Make sure projectile scale is always positive after unparenting
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) && 
            !ReferenceEquals(parent.gameObject, other.gameObject))
        {
            other.gameObject.GetComponent<ICharacter>().UpdateHealth(pointDamage, heartDamage, knockbackForce * new Vector2(direction.x, 1));
        }
    }
}
