/* Heart.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-24
 * 
 * Binds healing skill to a button.  Does not really function as skill.
 * Doesn't work with button on click event.
 * 
 * 2020-11-24: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Heart", menuName = "Skills/Heart")]
public class Heart : ISkill
{
    public override void Attach(GameObject _owner)
    {
        owner = _owner;
    }

    public override void DeductCost()
    {
    }

    public override void Detach()
    {
    }

    public override void Initialize()
    {
        SetCostLabel();
    }

    public override void SetCostLabel()
    {
        costLabel.text = "";
    }

    public override void SkillUse()
    {
    }

    public override void AttachButton(Button button)
    {
        Heal healScript = button.gameObject.AddComponent<Heal>();
    }
}
