/* ISkill.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-23
 * 
 * Base class for skills.
 * 
 * 2020-11-23: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISkill : ScriptableObject
{
    public GameObject owner;
    public int cost;

    public abstract void Attach(GameObject _owner);
    public abstract void SkillUse();
    public abstract void DeductCost();
}
