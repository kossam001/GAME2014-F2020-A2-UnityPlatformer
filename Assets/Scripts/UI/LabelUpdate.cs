/* LabelUpdate.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * Updates HUD.
 * 
 * 2020-11-22: Added this script.
 * 2020-11-22: Changes hearts and score during gameplay.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabelUpdate : MonoBehaviour
{
    public TMP_Text scoreLabel;
    public TMP_Text heartLabel;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.onStatUpdated.AddListener(UpdateLabel);
    }

    private void UpdateLabel(int score, int hearts)
    {
        heartLabel.text = hearts + "%";
        scoreLabel.text = score.ToString();
    }
}
