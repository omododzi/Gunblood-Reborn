using System;
using UnityEngine;
using TMPro;
using YG;

public class ScoreController : MonoBehaviour
{
    public TMP_Text scoreText;
    public static int score;

    void Awake()
    {
        score = YandexGame.savesData.money;
    }

    private void FixedUpdate()
    {
        scoreText.text = score.ToString();
        YandexGame.savesData.money = score;
    }
}
