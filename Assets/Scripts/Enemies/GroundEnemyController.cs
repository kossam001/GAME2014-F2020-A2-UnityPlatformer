using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyController : MonoBehaviour
{
    public Transform lineEndpoint;
    public LayerMask layerMask;

    private Rigidbody2D rigidbody2d;

    public bool isTherePlatform;
    public float speed;
    public float direction;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        direction = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookForPlatformEdge();
        Move();
    }

    private void LookForPlatformEdge()
    {
        isTherePlatform = Physics2D.Linecast(transform.position, lineEndpoint.position, layerMask);

        Debug.DrawLine(transform.position, lineEndpoint.position, Color.green);
    }

    private void Move()
    {
        if (isTherePlatform)
        {
            rigidbody2d.AddForce(Vector2.left * speed * Time.deltaTime * direction);
        }
        else
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            direction *= -1;
        }

        rigidbody2d.velocity *= 0.90f;
    }
}
