using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesManagerComponent : MonoBehaviour
{
    public GameObject[] collectibles;
    private PlayerMovementComponent playerMovementComponent;
    public bool shouldSpawnNextCollectible = true;
    // Start is called before the first frame update
    void Start()
    {
        playerMovementComponent = FindObjectOfType<PlayerMovementComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        //collectibles spawn one at a time
        if (shouldSpawnNextCollectible)
        {
            SpawnCollectible();
            shouldSpawnNextCollectible = false;
        }
    }

    void SpawnCollectible()
    {

        int spawnType = Random.Range(0, collectibles.Length);
        Vector3 positionToSpawnAt;
        GameObject spawned;
        switch (spawnType)
        {

            //coin
            case 0:
                positionToSpawnAt = new Vector3(Random.Range(-7, 8), 1.5f, Random.Range(playerMovementComponent.gameObject.transform.position.z + 150, playerMovementComponent.gameObject.transform.position.z + 300));
                spawned = Instantiate(collectibles[0]);
                spawned.SetActive(true);
                spawned.transform.position = positionToSpawnAt;
                break;

            //shield
            case 1:
                positionToSpawnAt = new Vector3(Random.Range(-7, 8), 1.5f, Random.Range(playerMovementComponent.gameObject.transform.position.z + 150, playerMovementComponent.gameObject.transform.position.z + 300));
                spawned = Instantiate(collectibles[1]);
                spawned.SetActive(true);
                spawned.transform.position = positionToSpawnAt;
                break;

        }
    }
}
