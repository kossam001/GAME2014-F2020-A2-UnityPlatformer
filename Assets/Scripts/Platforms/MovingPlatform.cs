/* MovingPlatform.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-23
 * 
 * Platform that moves back and forth.
 * 
 * 2020-11-23: Added this script.  Objects will be knocked off the platform when they are clipped between platforms.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 5;
    public float maxMovementDistance = 100;
    public Vector2 direction;
    public float brakeSpeed = 1;
    public float startingDistance;
    public int directionSwitch = 1;

    private float cumulativeDistance = 0;
    private Rigidbody2D rigidbody2d;
    private float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 5;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody2d.MovePosition(rigidbody2d.position + direction * speed * Time.fixedDeltaTime);
        cumulativeDistance += directionSwitch * speed;

        // Apply brakes
        // Want gradual decceleration to pevent objects from being launched
        if ((cumulativeDistance > maxMovementDistance) ||
            (cumulativeDistance < 0) ||
            speed < maxSpeed)
        {
            speed -= brakeSpeed * Time.fixedDeltaTime;

            // Once speeds have hit reverse, we can switch the movement direction
            if (Mathf.Abs(speed) >= maxSpeed)
            {
                speed = maxSpeed;
                direction *= -1;
                directionSwitch *= -1;
            }
        }
    }
}
