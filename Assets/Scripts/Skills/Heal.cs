/* Heal.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-26
 * 
 * Reduces heart score by holding down the skill button.
 * 
 * 2020-11-24: Added this script.
 * 2020-11-25: Fixed bug where hearts can go negative.
 * 2020-11-26: Stopped healing from going negative.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Heal : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed;

    // Start is called before the first frame update
    public void OnUpdateSelected(BaseEventData data)
    {
        if (isPressed)
        {
            if (GameManager.Instance.playerHeart > 0)
            {
                GameManager.Instance.UpdateHealth(-1, -1);
            }
        }
    }
    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
    }
}
