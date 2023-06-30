using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectibleCoinComponent : MonoBehaviour
{
    public float scoreAdd;
    private CollectiblesManagerComponent collectiblesManagerComponent;
    private GameObject player;
    private float currentTimeToDeactivate = -1.0f;
    public float timeToDeactivate;
    public TMP_Text addScoreText;
    // Start is called before the first frame update
    void Start()
    {
        collectiblesManagerComponent = FindObjectOfType<CollectiblesManagerComponent>();
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //player missing the coin causes the coin to despawn and allows new coin to be spawned
        if (player.transform.position.z > transform.position.z + 10)
        {
            collectiblesManagerComponent.shouldSpawnNextCollectible = true;
            gameObject.SetActive(false);
        }

        //coins play animation when collected
        //the timer makes sure that the animation is fully played before the coin is considered collected by the player
        if (currentTimeToDeactivate != -1.0f)
        {
            if (currentTimeToDeactivate > 0.0f)
            {
                currentTimeToDeactivate -= Time.deltaTime;
            }

            else
            {
                addScoreText.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if coin collides with the player then coin is collected by the player
        //collecting the coin increases player's score
        if (other.tag == "Player")
        {
            FindObjectOfType<ScoreManagerComponent>().scoreInt += scoreAdd;
            collectiblesManagerComponent.shouldSpawnNextCollectible = true;
            GetComponent<Animator>().SetBool("Collected", true);
            currentTimeToDeactivate = timeToDeactivate;
            addScoreText.gameObject.SetActive(true);
            addScoreText.text = scoreAdd.ToString();
            addScoreText.text = ("+" + addScoreText.text);
        }
    }
}
