/* RangedAttack.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-21
 * 
 * Player will have a boomerang attack.  This script sits between
 * the user control and game logic.
 * 
 * 2020-11-21: Added this script.
 * 2020-11-21: Determines throw direction based on reticle and player position.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedAttack : MonoBehaviour
{
    public Boomerang boomerang;
    public Button attackButton;
    public GameObject weapon;
    public Canvas canvas;
    public DynamicJoystick reticle; // Joystick is used to determine throwing direction

    private RectTransform reticleTransform;
    private bool isAiming = false;

    // Start is called before the first frame update
    void Start()
    {
        reticleTransform = reticle.gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        boomerang.onPlayerCatch.AddListener(PlayerWeaponReturned);
    }

    // Update is called once per frame
    void Update()
    {
        // DynamicJoystick is hidden until the touch region is touched
        if (reticleTransform.gameObject.activeInHierarchy)
        {
            isAiming = true;
        }
        // On release of the DynamicJoystick, the reticle becomes in active again
        else if (!reticleTransform.gameObject.activeInHierarchy && isAiming)
        {
            isAiming = false;

            // Reticle localPosition is the center of the screen, where the player is
            StartCoroutine(boomerang.Throw(reticleTransform.localPosition.normalized));
            weapon.SetActive(false); // Hide the weapon sprite
            attackButton.interactable = false; // Disable attack button
        }
    }

    void PlayerWeaponReturned()
    {
        weapon.SetActive(true);
        attackButton.interactable = true;
    }
}
