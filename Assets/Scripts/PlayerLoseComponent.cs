using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLoseComponent : MonoBehaviour
{
    public bool shielded = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            //if player has picked up shield he cannot die
            if (!shielded)
            {
                //if player doesn't have shield and collides with the obstacle he loses the game.
                GetComponent<PlayerLoseComponent>().enabled = false;
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

}
