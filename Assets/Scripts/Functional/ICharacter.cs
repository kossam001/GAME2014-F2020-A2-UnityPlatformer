/* ICharacter.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-21
 * 
 * Generalize players and enemies.
 * 
 * 2020-11-21: Added this script.
 * 2020-11-21: Is considered a dynamic object.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacter : SpawnableObject
{
    public AudioClip hitSound;
    public AudioClip boostSound;
    public AudioClip attackSound;
    public AudioSource audioPlayer;

    public abstract void UpdateHealth(int pointLoss, int heartGain, Vector2 knockbackForce);
}
