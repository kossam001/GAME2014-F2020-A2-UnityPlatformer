/* DoubleJump.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-24
 * 
 * A skill to allow player to jump again when in the air.
 * 
 * 2020-11-23: Added this script.
 * 2020-11-24: Added additional management functions.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DoubleJump", menuName = "Skills/DoubleJump")]
public class DoubleJump : ISkill
{
    private PlayerController playerController;
    private int jumpCount = 0;

    public override void Attach(GameObject _owner)
    {
        owner = _owner;
    }

    public override void DeductCost()
    {
        GameManager.Instance.UpdateHealth(-cost, 0);
    }

    public override void Detach()
    {
        playerController.onJump.RemoveListener(SkillUse);
    }

    public override void Initialize()
    {
        playerController = owner.GetComponent<PlayerController>();
        playerController.onJump.AddListener(SkillUse);

        SetCostLabel();
    }

    public override void SetCostLabel()
    {
        costLabel.text = cost.ToString();
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

            DeductCost();
        }
        else if (jumpCount > 0 && playerController.isGrounded)
        {
            jumpCount = 0;
        }
    }
}
