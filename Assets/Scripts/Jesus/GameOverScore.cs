using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScore : MonoBehaviour
{
    float score;
    [SerializeField] protected TextMeshProUGUI scoreText;

    private void Start()
    {
        score = PlayerPrefs.GetFloat("CurrentScore");
        scoreText.text = "People Saved:  " + score.ToString("0");
    }
}