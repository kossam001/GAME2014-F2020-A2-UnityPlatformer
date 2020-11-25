/* Pickup.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-24
 * 
 * Method by which players acquire skills - by using item pickups.
 * 
 * 2020-11-24: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pickup : MonoBehaviour
{
    public Canvas skillPanel; // Has slots for the skills
    public Canvas erasePanel;
    public ISkill skill;
    public GameObject skillButtonTemplate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Transform slot;

            for (int i = 0; i < skillPanel.gameObject.transform.childCount; i++)
            {
                // Find an empty slot
                slot = skillPanel.gameObject.transform.GetChild(i);
                if (slot.childCount <= 0)
                {
                    gameObject.SetActive(false);
                    GameObject button = CreateSkillButton(other.gameObject);
                    button.transform.SetParent(slot);

                    // Pass skill reference to erase skill panel
                    GameObject skillEraseButton = erasePanel.transform.GetChild(i).gameObject;
                    skillEraseButton.GetComponent<DeleteSkill>().skill = skill;

                    // Reposition the button
                    RectTransform buttonTransform = button.GetComponent<RectTransform>();
                    buttonTransform.anchoredPosition = new Vector3(0, button.GetComponent<RectTransform>().anchoredPosition.y);
                    break;
                }
            }
        }
    }

    public GameObject CreateSkillButton(GameObject owner)
    {
        GameObject skillButton = Instantiate(skillButtonTemplate);

        // Replace skill button image with the pickup icon.
        skillButton.GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;

        // Set the cost of the skill on the button
        skill.costLabel = skillButton.GetComponentInChildren<TMP_Text>();

        // Set owner of the skill
        skill.owner = owner;
        skill.Initialize();

        // Set button use for skill
        if (skill.isActiveSkill)
        {
            skill.AttachButton(skillButton.GetComponent<Button>());
        }
        else
        {
            skillButton.GetComponent<Button>().interactable = false;
        }

        return skillButton;
    }
}
