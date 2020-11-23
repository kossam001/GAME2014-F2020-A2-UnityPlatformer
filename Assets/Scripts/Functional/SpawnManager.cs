/* SpawnManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-22
 * 
 * Handles spawning of non-static objects.
 * 
 * 2020-11-22: Added this script.
 * 2020-11-22: Spawns rocks that drops from above the player and despawns some range away from the player.
 *             Despawned rocks gets returned to SpawnManager to be reused.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance;
    public static SpawnManager Instance { get { return instance; } }

    // Singleton
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public GameObject fixedEnemyPrefab;
    public GameObject fallingEnemyPrefab;
    public GameObject rockPrefab;
    public GameObject pointsPrefab;
    public GameObject tombstonePrefab;

    public Transform playerTransform;
    public float spawnHorizontalRange = 25;
    public float spawnHeight = 15;
    public bool onCooldown = false;

    [SerializeField]
    private Queue<FallingObject> fallingObjects;
    [SerializeField]
    private Queue<GameObject> fixedSpawnEnemies;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameManager.Instance.GetPlayerTransform();
        fallingObjects = new Queue<FallingObject>();

        for (int i = 0; i < 20; i ++)
        {
            GameObject rock = Instantiate(rockPrefab);
            FallingObject fallingComponent = rock.GetComponent<FallingObject>();
            fallingComponent.playerTransform = playerTransform;
            rock.SetActive(false);
            rock.transform.parent = gameObject.transform;

            fallingObjects.Enqueue(fallingComponent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DropRocks();
    }

    public void DropRocks()
    {
        if (fallingObjects.Count <= 0 || onCooldown)
        {
            return;
        }

        onCooldown = true;

        FallingObject rock = fallingObjects.Dequeue();

        Vector3 spawnRange = new Vector3(Random.Range(playerTransform.position.x - spawnHorizontalRange, 
            playerTransform.position.x + spawnHorizontalRange), 
            playerTransform.position.y + spawnHeight);

        rock.StartFalling(9999, true, false, true, spawnRange);

        StartCoroutine(RockCooldown());
    }

    public IEnumerator RockCooldown()
    {
        yield return new WaitForSeconds(5);
        onCooldown = false;
    }

    public void RetrieveDead(GameObject deadObject)
    {
        fallingObjects.Enqueue(deadObject.GetComponent<FallingObject>());
    }
}
