using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ObstacleComponent : MonoBehaviour
{
    public LayerMask groundLayerMask;
    public List<GameObject> obstaclesInTheSameRow = new List<GameObject>();
    private bool foundGround = false;
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 20, 0), transform.position + new Vector3(0, -150.0f, 0), out hit, Mathf.Infinity, groundLayerMask))
        {
            foundGround = true;
            return;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 20, 0), transform.position + new Vector3(0, -150.0f, 0), out hit, Mathf.Infinity, groundLayerMask))
        {
            foundGround = true;
            return;
        }

        if (foundGround)
        {
            for (int i = 0; i < obstaclesInTheSameRow.Count; i++)
            {
                obstaclesInTheSameRow[i].SetActive(false);
            }
        }
    }
}
