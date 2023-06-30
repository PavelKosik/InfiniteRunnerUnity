using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollectibleComponent : MonoBehaviour
{
    public float protectedTime;
    private float currentProtectedTime = -1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //makes sure the shield doesn't last forever
        //and removes the shield effect once the time runs out
        if (currentProtectedTime != -1.0f)
        {
            if (currentProtectedTime >= 0.0f)
            {
                currentProtectedTime -= Time.deltaTime;
            }

            else
            {
                FindObjectOfType<PlayerLoseComponent>().shielded = false;
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if shield collides with the player
        //this causes player to be shielded
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerLoseComponent>().shielded = true;
            currentProtectedTime = protectedTime;
            transform.SetParent(other.transform);
            transform.localPosition = new Vector3(0, 1, 1);
            transform.localScale = new Vector3(4, 4, 4);
            transform.localEulerAngles = new Vector3(0, 0, 0);
            GetComponentInChildren<ParticleSystem>().Stop();

        }
    }
}
