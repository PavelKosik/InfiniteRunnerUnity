using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformEndComponent : MonoBehaviour
{
    public GameObject parent;
    private PlatformManagerComponent platformManagerComponent;
    private ObstaclesManagerComponent obstaclesManagerComponent;
    // Start is called before the first frame update
    void Start()
    {
        platformManagerComponent = FindObjectOfType<PlatformManagerComponent>();
        obstaclesManagerComponent = FindObjectOfType<ObstaclesManagerComponent>();
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //each time player reaches the end of the platform the platform is movement behind the last platform
        //this makes it that no new platforms have to be spawned for the game to be infinite
        if (other.gameObject.tag == "Player")
        {
            //the platforms are of 2 types which have different sizes at different positions because of that
            //this makes sure that the platforms are positioned correctly regardless of their type
            if (platformManagerComponent.shouldChangePos)
            {
                platformManagerComponent.nextPlatformPosition = platformManagerComponent.nextPlatformPosition + new Vector3(0, 0, 80);
            }
            parent.transform.position = platformManagerComponent.nextPlatformPosition;
            platformManagerComponent.shouldChangePos = !platformManagerComponent.shouldChangePos;
        }
    }
}
