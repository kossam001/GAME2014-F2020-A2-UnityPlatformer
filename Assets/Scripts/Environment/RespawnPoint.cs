/* RespawnPoint.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-24
 * 
 * Stores number of points cached at a particular checkpoint.  Need to
 * attach to a GameObject with one or more SpriteRenderers otherwise not very useful.
 * 
 * 2020-11-24: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rather pointless to make it MonoBehaviour, but need the SpriteRenderers to set markers in the game
public class RespawnPoint : MonoBehaviour
{
    public int cachedPoints;
}
