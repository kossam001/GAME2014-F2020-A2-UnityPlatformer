/* GameOverLabel.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-30
 * 
 * Loads player's points and resets status.
 * 
 * 2020-11-30: Added this script.
 * 2020-11-30: Sets score label before resetting the score.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverLabel : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreLabel;

    private void Start()
    {
        scoreLabel.text = GameManager.Instance.playerScore.ToString();
        GameManager.Instance.ResetPlayer();
    }
}
