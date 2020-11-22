/* GameManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * Communications between different game components.
 * 
 * 2020-11-21: Added this script.
 * 2020-11-21: Moved player health and score hear so that there are no duplications.
 * 2020-11-22: Calls event to update label.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // Singleton
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

    private PlayerController player;
    public int playerScore = 100;
    public int playerHeart = 0;

    public UnityEvent<int,int> onStatUpdated;

    public void SetPlayer(PlayerController playerController)
    {
        player = playerController;
    }

    public int UpdateHealth(int pointsGain, int heartsIncrease)
    {
        playerScore += pointsGain;
        playerHeart += heartsIncrease;

        onStatUpdated.Invoke(playerScore, playerHeart);
        return playerHeart;
    }

    public void ResetPlayer()
    {

    }
}
