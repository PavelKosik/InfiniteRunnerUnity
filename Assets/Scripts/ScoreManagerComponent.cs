using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManagerComponent : MonoBehaviour
{
    public TMP_Text scoreText;
    public string score;
    [HideInInspector]
    public float scoreInt = 0.0f;
    public float scoreMultiplier = 0.1f;
    private PlayerMovementComponent playerMovementComponent;
    // Start is called before the first frame update
    void Start()
    {
        playerMovementComponent = FindObjectOfType<PlayerMovementComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        //score is increased every frame
        scoreInt += Time.deltaTime * playerMovementComponent.moveSpeedForward * scoreMultiplier;
        score = Mathf.Round(scoreInt).ToString();
        int digits = score.Length;
        //makes sure that every digit not used is display as a 0
        //for example 00123
        for (int i = digits; i < 5; i++)
        {
            score = score.Insert(0, "0");
        }
        scoreText.text = score;
    }
}
