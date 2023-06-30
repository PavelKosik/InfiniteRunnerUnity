using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesManagerComponent : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject blockObstaclePrefab;
    public GameObject obstacleSlidePrefab;
    public Vector3 positionToSpawnTheObstacleAt;
    private PlatformManagerComponent platformManagerComponent;
    public float playerDistanceToPass;
    private GameObject player;
    public float speedDifference = 0.0f;
    public GameObject obstacleParent;
    public Vector3 obstaclePos;
    public Vector3 obstacleSlidePos;
    // Start is called before the first frame update
    void Start()
    {
        //spawns the first line of obstacles
        platformManagerComponent = FindObjectOfType<PlatformManagerComponent>();
        for (int i = 0; i < 5; i++)
        {
            SpawnObstacles();
        }
        player = FindObjectOfType<PlayerMovementComponent>().gameObject;

        //sets new distance for player to pass to spawn new obstacåes
        if (player)
        {
            playerDistanceToPass = player.transform.position.z + 50;
        }

        else
        {
            player = FindObjectOfType<PlayerMovementComponent>().gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //speed difference makes sure that player has still time to react when the game gets faster
        speedDifference += Time.deltaTime;
        if (player)
        {
            //if player passes a certain distance new obstacles are spawned
            if (player.transform.position.z > playerDistanceToPass)
            {
                SpawnObstacles();
                playerDistanceToPass += 50;
            }
        }

        else
        {
            player = FindObjectOfType<PlayerMovementComponent>().gameObject;
        }
    }

    public void SpawnObstacles()
    {
        positionToSpawnTheObstacleAt += new Vector3(0, 0, 50);
        //number of obstacles in the row
        int numberOfObstaclesToSpawn = Random.Range(3, 6);
        //blocked obstacles cannot be passed through and have to be gone around
        List<bool> shouldBeBlocked = new List<bool>();
        List<int> indexOfObstacleToSpawn = new List<int>();
        int typeOfObstacle = Random.Range(0, 3); // 0 == normal obstacle, 1 == blocked obstacle, 2 == slide obstacle
        for (int i = 0; i < numberOfObstaclesToSpawn; i++)
        {
            bool added = false;

            //searches for an index which isn't used in the current row
            //index determinates the horizontal position of the obstacle
            while (!added)
            {
                int indexToAdd = Random.Range(0, 5);

                if (indexOfObstacleToSpawn.Contains(indexToAdd) == false)
                {
                    //if the obstacle was selected to be blocked it's added to the block list
                    if (typeOfObstacle == 1)
                    {
                        shouldBeBlocked.Add(true);
                    }

                    else
                    {
                        shouldBeBlocked.Add(false);
                    }

                    indexOfObstacleToSpawn.Add(indexToAdd);
                    added = true;
                }
            }
        }
        bool atleastOneFalse = false;
        //checks if at least one obstacle is blocked
        for (int i = 0; i < shouldBeBlocked.Count; i++)
        {
            if (!shouldBeBlocked[i])
            {
                atleastOneFalse = true;
                break;
            }
        }
        //makes sure that there is at least 1 obstacle that is not blocked
        //otherwise player couldn't pass through the obstacles
        if (!atleastOneFalse)
        {
            int notBlockedIndex = Random.Range(0, shouldBeBlocked.Count);
            shouldBeBlocked[notBlockedIndex] = false;
        }

        int numberOfSpawned = 0;
        List<GameObject> spawnedObstacles = new List<GameObject>();

        //spawns the proper type of obstacle on it's proper position based on it's index
        for (int x = 0; x < 5; x++)
        {
            if (indexOfObstacleToSpawn.Contains(x))
            {
                GameObject currentObstacle;
                if (!shouldBeBlocked[numberOfSpawned])
                {
                    if (typeOfObstacle == 0)
                    {
                        currentObstacle = Instantiate(obstaclePrefab);
                        positionToSpawnTheObstacleAt = new Vector3(positionToSpawnTheObstacleAt.x, obstaclePos.y, positionToSpawnTheObstacleAt.z);
                    }

                    else
                    {
                        currentObstacle = Instantiate(obstacleSlidePrefab);
                        positionToSpawnTheObstacleAt = new Vector3(positionToSpawnTheObstacleAt.x, obstacleSlidePos.y, positionToSpawnTheObstacleAt.z);
                    }
                }

                else
                {
                    currentObstacle = Instantiate(blockObstaclePrefab);
                    positionToSpawnTheObstacleAt = new Vector3(positionToSpawnTheObstacleAt.x, obstaclePos.y, positionToSpawnTheObstacleAt.z);
                }
                numberOfSpawned++;
                currentObstacle.transform.position = new Vector3(-6.5f + (3.7f * x), positionToSpawnTheObstacleAt.y, positionToSpawnTheObstacleAt.z);
                currentObstacle.transform.SetParent(obstacleParent.transform);
                spawnedObstacles.Add(currentObstacle);
            }
        }
        spawnedObstacles[0].GetComponent<ObstacleComponent>().obstaclesInTheSameRow = spawnedObstacles;
    }
}
