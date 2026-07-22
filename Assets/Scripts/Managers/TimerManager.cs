using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static int seconds;
    public static int minitues;
    public static bool isNewHighScore;
    public TMP_Text timerText;
    private void Start()
    {
        StartCoroutine(Timer());
        StartCoroutine(RandomStone());
        seconds = 0;
        minitues = 0;
        isNewHighScore = false;
        timerText.transform.parent.DOLocalMoveY(400, 5f).SetEase(Ease.OutQuint);
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            seconds++;
            if (seconds >= 60)
            {
                minitues++;
                seconds = 0;
            }
            timerText.text = (minitues == 0 ? "0" : minitues.ToString()) + ":" + (seconds < 10 ? "0" + seconds : seconds);
        }
    }
    IEnumerator RandomStone()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);
            GameManager.instance.resourceManager.GetRandomStone();
        }
    }
    void OnEnable()
    {
        GetComponentInParent<GameManager>().onGameOver += GameOver;
    }
    void OnDisable()
    {
        GameManager.instance.onGameOver -= GameOver;
    }
    void GameOver()
    {
        StopAllCoroutines();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            int timeInSec = minitues * 60 + seconds;
            if (PlayerPrefs.GetInt("HighScore") < timeInSec)
            {
                SetHighScore();
            }
        }
        else
        {
            SetHighScore();
        }
    }
    void SetHighScore()
    {
        isNewHighScore = true;
        PlayerPrefs.SetString("HighScoreText", timerText.text);
        int timeInSec = minitues * 60 + seconds;
        PlayerPrefs.SetInt("HighScore", timeInSec);
        Debug.Log("New High Score : " + timerText.text);
    }
}
