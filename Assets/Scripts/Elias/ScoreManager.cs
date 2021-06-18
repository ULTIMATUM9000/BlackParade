using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static float Score;

    void Start()
    {
        Score = 0;
    }

    public void AddScore(float value)
    {
        Score += value;
        PlayerPrefs.SetFloat("CurrentScore", Score);
        //Debug.Log("SCORE is " + Score);
    }

    // public void RestartTimer(int damage)
    // {
    //     _TempScore += damage;

    //     if (!_SlimeDamage)
    //         StopCoroutine(Timer());

    //     _SlimeDamage = true;

    //     StartCoroutine(Timer());
    // }

    // IEnumerator Timer()
    // {
    //     yield return new WaitForSeconds(3);

    //     _SlimeDamage = false;
    //     Score += _TempScore;
    //     _TempScore = 0;
    // }
}
