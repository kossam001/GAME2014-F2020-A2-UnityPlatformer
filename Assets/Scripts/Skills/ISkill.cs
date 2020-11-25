/* ISkill.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-24
 * 
 * Base class for skills.
 * 
 * 2020-11-23: Added this script.
 * 2020-11-24: Added additional methods to enhance functionality of skills.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class ISkill : ScriptableObject
{
    public bool isActiveSkill;
    public GameObject owner;
    public TMP_Text costLabel;
    public int cost;

    public abstract void Attach(GameObject _owner);
    public abstract void Detach();

    public abstract void SetCostLabel();
    public abstract void Initialize();
    public abstract void SkillUse();
    public abstract void DeductCost();

    public virtual void AttachButton(Button button) { }
}
