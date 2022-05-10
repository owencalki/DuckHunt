using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public Text scoreText;
    public int scoreNum = 0;
    int ducksRemaining;
    float t = 0f;

    private void Update()
    {
        ducksRemaining = FindObjectsOfType<duck>().Length;
        scoreText.text = ducksRemaining.ToString();
        if (ducksRemaining == 0 && Time.time-t>5f) 
        {
            t = Time.time;
            StartCoroutine(FindObjectOfType<RoundManager>().EndRound());
        }
    }
}
