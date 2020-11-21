/* ICharacter.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-21
 * 
 * Generalize players and enemies.
 * 
 * 2020-11-21: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacter : MonoBehaviour
{
    public abstract void UpdateHealth(int pointLoss, int heartGain, Vector2 knockbackForce);
}
