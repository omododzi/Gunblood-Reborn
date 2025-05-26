using System;
using UnityEngine;
using TMPro;
public class ScoreController : MonoBehaviour
{
    public TMP_Text scoreText;
    public static int score;

    void Start()
    {
        score = 30;
    }

    private void FixedUpdate()
    {
        scoreText.text = score.ToString();
    }
}
