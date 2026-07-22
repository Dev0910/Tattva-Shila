using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenUi : MonoBehaviour
{
    [SerializeField] TMP_Text myScore;
    [SerializeField] TMP_Text highScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (TimerManager.isNewHighScore)
        {
            highScore.text = "New High Score";
        }
        else
        {
            highScore.text = "High Score: " + TimerManager.minitues + ":" + TimerManager.seconds;

        }
        myScore.text = PlayerPrefs.GetString("HighScoreText");
    }
    public void LoadSeneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

}
