/* GameManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-21
 * 
 * Communications between different game components.
 * 
 * 2020-11-21: Added this script.
 * 2020-11-21: Moved player health and score hear so that there are no duplications.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer(PlayerController playerController)
    {
        player = playerController;
    }

    public int UpdateHealth(int pointsGain, int heartsIncrease)
    {
        playerScore += pointsGain;
        return playerHeart += heartsIncrease;
    }
}
