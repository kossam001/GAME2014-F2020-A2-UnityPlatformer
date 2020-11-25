/* DeleteSkill.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-25
 * 
 * Unbinds the skill on the referenced button.  Calls the skills Detach method.
 * 
 * 2020-11-25: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteSkill : MonoBehaviour
{
    public GameObject skillButton;

    public void UnbindSkill()
    {
        ISkill skill = skillButton.GetComponent<RegisteredSkill>().skill;
        skill.Detach();

        skillButton.GetComponent<Button>().interactable = true;
        skillButton.SetActive(false);
    }
}
