/* SpawnableObject.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * Used to generalize all non-static elements in the game and have them share common functionality.
 * 
 * 2020-11-22: Added this script.
 * 2020-11-22: All dynamic objects should be resetable.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    public abstract void Reset();
}
