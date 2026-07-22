using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject target;
    public bool gameOver;
    public TimerManager timerManager;
    public SpawnMannager spawnMannager;
    public ResourceManager resourceManager;

    public delegate void OnGameOver();
    public event OnGameOver onGameOver;
    void Awake()
    {
        Cursor.visible = true;
        instance = this;
        target = GameObject.Find("Target");

        timerManager = GetComponentInChildren<TimerManager>();
        spawnMannager = GetComponentInChildren<SpawnMannager>();
        resourceManager = GetComponentInChildren<ResourceManager>();
    }
    void Start()
    {
        gameOver = false;
        AudioManager.Instance.PlayMusic("InGame");
    }
    void ChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        gameOver = true;
        onGameOver?.Invoke();
        Invoke(nameof(ChangeScene), 1f);
    }
}
