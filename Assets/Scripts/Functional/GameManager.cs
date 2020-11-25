/* GameManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-24
 * 
 * Communications between different game components.
 * 
 * 2020-11-21: Added this script.
 * 2020-11-21: Moved player health and score hear so that there are no duplications.
 * 2020-11-22: Calls event to update label.
 * 2020-11-22: Added a global access to player transform here
 * 2020-11-24: Implemented respawn mechanic.
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

    public GameObject player;
    public int playerScore = 100;
    public int playerHeart = 0;

    public Queue<GameObject> availableRespawnPoints;
    public List<GameObject> activeRespawnPoints;

    public UnityEvent<int,int> onStatUpdated;

    private void Start()
    {
        availableRespawnPoints = new Queue<GameObject>();
        activeRespawnPoints = new List<GameObject>();
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
        if (activeRespawnPoints.Count > 0)
        {
            // Get most recently added item
            GameObject respawnPoint = activeRespawnPoints[activeRespawnPoints.Count-1];

            player.transform.position = respawnPoint.transform.position;

            // Point calculation
            playerScore = 0;
            int cachedPoints = respawnPoint.GetComponent<RespawnPoint>().cachedPoints;
            UpdateHealth(cachedPoints, 0); // Just to invoke update event
            respawnPoint.GetComponent<RespawnPoint>().cachedPoints = 0;
            player.GetComponent<PlayerController>().totalFallDistance = 9999; // Prevent point loss from fall damage

            respawnPoint.SetActive(false);
            availableRespawnPoints.Enqueue(respawnPoint);

            activeRespawnPoints.RemoveAt(activeRespawnPoints.Count - 1);
        }
        // Game Over
        else
        {
            player.SetActive(false);
        }
    }

    public Transform GetPlayerTransform()
    {
        return player.transform;
    }
}
