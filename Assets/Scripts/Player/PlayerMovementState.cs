/* PlayerMovementState.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-15
 * 
 * Primarily used for transitioning between states in the Animator.
 * 
 * 2020-11-15: Added this script.
 * 2020-11-15: Added some states.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayerMovementState
{
    IDLE,
    RUN,
    JUMP,
    ATTACK
}
