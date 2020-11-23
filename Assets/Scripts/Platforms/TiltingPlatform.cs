/* TiltingPlatform.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-23
 * 
 * Standing on the ends of this platform will tilt it.  This script puts the platform back 
 * to a stable position if nothing is on it.
 * 
 * 2020-11-23: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltingPlatform : MonoBehaviour
{
    public int rotationSpeed = 1;
    private bool hasObjectWeighingDown = false;
    private Rigidbody2D rigidbody2d;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckRotationVelocity();
        RestorePosition();
    }

    void RestorePosition()
    {
        if (rigidbody2d.rotation > 1 && !hasObjectWeighingDown)
        {
            rigidbody2d.MoveRotation(rigidbody2d.rotation - rotationSpeed * Time.fixedDeltaTime);
        }
        else if (rigidbody2d.rotation < -1 && !hasObjectWeighingDown)
        {
            rigidbody2d.MoveRotation(rigidbody2d.rotation + rotationSpeed * Time.fixedDeltaTime);
        }

    }

    void CheckRotationVelocity()
    {
        if (rigidbody2d.angularVelocity == 0)
        {
            hasObjectWeighingDown = false;
            return;
        }

        hasObjectWeighingDown = true;
    }
}
