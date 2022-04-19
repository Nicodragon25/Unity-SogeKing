using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int actualScore = 0;

    private void Awake()
    {
        actualScore = 0;
    }
    public void AddScore(int points)
    {
        if (GameObject.Find("ScoreTxt") != null)
        {
            scoreText = GameObject.Find("ScoreTxt").GetComponent<TextMeshProUGUI>();
            scoreText.text = "Score : " + actualScore;
        }
        actualScore += points;
        scoreText.text = "Score : " + actualScore.ToString();
        if (actualScore >= PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", actualScore);
        }
    }
}
