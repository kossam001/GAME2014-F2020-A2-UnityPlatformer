/* SpawnManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-11-30
 * 
 * Handles spawning of non-static objects.
 * 
 * 2020-11-22: Added this script.
 * 2020-11-22: Spawns rocks that drops from above the player and despawns some range away from the player.
 *             Despawned rocks gets returned to SpawnManager to be reused.
 * 2020-11-28: Spawner does not manage fixed spawns.  Airborne enemies spawn every 30 seconds of despawning.
 * 2020-11-28: Fixed bat spawning issue.  It starts spawned in, and eventually teleports.
 * 2020-11-30: Added fixed enemy spawning.
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

    public GameObject airEnemyPrefab;
    public GameObject fallingEnemyPrefab;
    public GameObject rockPrefab;
    public GameObject pointsPrefab;
    public GameObject tombstonePrefab;

    public Transform playerTransform;
    public float spawnHorizontalRange = 25;
    public float spawnHeight = 15;
    public bool onCooldown = false;

    public int numFallingEnemy = 5;
    public int numTombstone = 5;
    public int numRocks = 10;
    public int numPoints = 10;

    [SerializeField]
    private List<FallingObject> fallingObjects;
    [SerializeField]
    private Queue<GameObject> airSpawnEnemies;
    [SerializeField]
    private List<GameObject> fixedSpawns;

    private float airEnemySpawnTimer = 30;
    private float enemySpawnTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameManager.Instance.GetPlayerTransform();
        fallingObjects = new List<FallingObject>();
        FallingObject fallingComponent;

        // Rocks
        for (int i = 0; i < numRocks; i++)
        {
            GameObject rock = Instantiate(rockPrefab);
            fallingComponent = rock.GetComponent<FallingObject>();
            fallingComponent.playerTransform = playerTransform;
            rock.SetActive(false);
            rock.transform.parent = gameObject.transform;

            fallingObjects.Add(fallingComponent);
        }

        // Points
        for (int i = 0; i < numPoints; i++)
        {
            GameObject points = Instantiate(pointsPrefab);
            fallingComponent = points.GetComponent<FallingObject>();
            fallingComponent.playerTransform = playerTransform;
            points.SetActive(false);
            points.transform.parent = gameObject.transform;

            fallingObjects.Add(fallingComponent);
        }

        // Tombstone
        for (int i = 0; i < numTombstone; i++)
        {
            GameObject tombstone = Instantiate(tombstonePrefab);
            fallingComponent = tombstone.GetComponent<FallingObject>();
            fallingComponent.playerTransform = playerTransform;
            tombstone.SetActive(false);
            tombstone.transform.parent = gameObject.transform;

            fallingObjects.Add(fallingComponent);
        }

        // AI
        for (int i = 0; i < numFallingEnemy; i++)
        {
            GameObject ai = Instantiate(fallingEnemyPrefab);
            fallingComponent = ai.GetComponent<FallingObject>();
            fallingComponent.playerTransform = playerTransform;
            ai.SetActive(false);
            ai.transform.parent = gameObject.transform;

            fallingObjects.Add(fallingComponent);
        }

        airSpawnEnemies = new Queue<GameObject>();
        GameObject airborneEnemy = Instantiate(airEnemyPrefab);
        airborneEnemy.SetActive(false);
        airSpawnEnemies.Enqueue(airborneEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        DropObjects();
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        airEnemySpawnTimer -= Time.deltaTime;
        enemySpawnTimer -= Time.deltaTime;

        if (airSpawnEnemies.Count > 0 && airEnemySpawnTimer <= 0)
        {
            GameObject enemy = airSpawnEnemies.Dequeue();

            Vector3 spawnRange = new Vector3(Random.Range(playerTransform.position.x - spawnHorizontalRange,
                playerTransform.position.x + spawnHorizontalRange),
                Random.Range(playerTransform.position.y - spawnHeight,
                playerTransform.position.y + spawnHeight));

            enemy.transform.position = spawnRange;

            enemy.GetComponent<AirEnemyController>().Reset();
        }

        if (enemySpawnTimer <= 0)
        {
            foreach (GameObject enemy in fixedSpawns)
            {
                // Enemies below get despawned, so a negative distance should not spawn
                float verticalDistance = enemy.transform.position.y - playerTransform.position.y;

                if (!enemy.activeInHierarchy && verticalDistance <= 25)
                {
                    float horizontalDistance = Mathf.Abs(enemy.transform.position.x - playerTransform.position.x);

                    if (verticalDistance >= -10 && horizontalDistance >= 20)
                    {
                        // Pick an enemy and respawn them
                        enemy.GetComponent<GroundEnemyController>().Reset();

                        // If respawn successful, break out of loop, otherwise try the next enemy.
                        if (enemy.activeInHierarchy)
                        {
                            enemySpawnTimer = 5;
                            return;
                        }
                    }
                }
            }
        }
    }

    public void DropObjects()
    {
        if (fallingObjects.Count <= 0 || onCooldown)
        {
            return;
        }

        onCooldown = true;

        // Select a random object from the list
        int randomInt = Random.Range(0, fallingObjects.Count - 1);
        FallingObject obj = fallingObjects[randomInt];
        fallingObjects.RemoveAt(randomInt);

        Vector3 spawnRange = new Vector3(Random.Range(playerTransform.position.x - spawnHorizontalRange, 
            playerTransform.position.x + spawnHorizontalRange), 
            playerTransform.position.y + spawnHeight);

        switch (obj.gameObject.GetComponent<SpawnableObject>().objType)
        {
            case EnumSpawnObjectType.ROCK:
                obj.StartFalling(Random.Range(0, 10), true, false, false, spawnRange);
                break;
            case EnumSpawnObjectType.AI:
                obj.StartFalling(Random.Range(0, 10), false, Random.value < 0.5f, true, spawnRange);
                break;
            case EnumSpawnObjectType.TOMBSTONE:
                obj.StartFalling(Random.Range(0, 4), true, Random.value < 0.25f, true, spawnRange);
                break;
            case EnumSpawnObjectType.POINTS:
                obj.StartFalling(Random.Range(0, 4), true, Random.value < 0.25f, false, spawnRange);
                break;
        }

        StartCoroutine(Cooldown());
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(Random.Range(0, 10));
        onCooldown = false;
    }

    public void RetrieveDead(GameObject deadObject)
    {
        if (deadObject.GetComponent<FallingObject>() != null)
        {
            fallingObjects.Add(deadObject.GetComponent<FallingObject>());
        }
        else if (deadObject.GetComponent<AirEnemyController>() != null)
        {
            airSpawnEnemies.Enqueue(deadObject);
            airEnemySpawnTimer = 30;
        }
    }
}
