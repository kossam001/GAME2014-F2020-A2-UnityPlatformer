/* DoubleJump.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-23
 * 
 * A skill to allow player to jump again when in the air.
 * 
 * 2020-11-23: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoubleJump", menuName = "Skills/DoubleJump")]
public class DoubleJump : ISkill
{
    private PlayerController playerController;
    private int jumpCount = 0;

    public override void Attach(GameObject _owner)
    {
        owner = _owner;

        playerController = owner.GetComponent<PlayerController>();
        playerController.onJump.AddListener(SkillUse);
    }

    public override void DeductCost()
    {
        throw new System.NotImplementedException();
    }

    public override void SkillUse()
    {
        if ((playerController.joystick.Vertical > playerController.verticalSensitivity) && playerController.isFalling && jumpCount <= 0)
        {
            playerController.rigidbody2d.velocity = Vector2.zero; // Negate the effects of gravity and reduce the compounding effect of the previous jump
            playerController.isFalling = false;
            playerController.totalFallDistance = 0; // Reset fall damage calculation
            playerController.cumulativeJumpForce = 0;
            jumpCount++;
        }
        else if (jumpCount > 0 && playerController.isGrounded)
        {
            jumpCount = 0;
        }
    }
}
