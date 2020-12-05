/* Pickup.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-25
 * 
 * Method by which players acquire skills - by using item pickups.
 * 
 * 2020-11-24: Added this script.
 * 2020-11-25: There is a fixed number of buttons, no need to instantiate new ones.
 * 2020-12-05: Fixing sound not playing.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pickup : MonoBehaviour
{
    //public Canvas skillPanel; // Has slots for the skills
    public GameObject[] buttons;
    public ISkill skill;
    public GameObject skillButtonTemplate;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().Play();
            foreach (GameObject button in buttons)
            {
                if (!button.activeInHierarchy)
                {
                    DisablePickup();

                    BindSkill(button, other.gameObject);
                    button.SetActive(true);

                    break;
                }
            }
        }
    }

    // Disabling the entire game object causes sound to not play, this is a workaround
    void DisablePickup()
    {
        this.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
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

    public void BindSkill(GameObject skillButton, GameObject owner)
    {
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

        skillButton.GetComponent<RegisteredSkill>().skill = skill;
    }
}
