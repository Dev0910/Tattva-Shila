using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class MenuManager : MonoBehaviour
{
    [Header("Main Menu Panels")]
    public GameObject menuPanel;
    [SerializeField] Button playButton;
    [SerializeField] Button exitButton;
    [SerializeField] Button creditsButton;
    [SerializeField] Image gameTitle1;
    [SerializeField] Image gameTitle2;
    [Header("Credits Panel")]
    public GameObject creditsPanel;

    private void Awake()
    {
        menuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
    void Start()
    {
        AudioManager.Instance.PlayMusic("Start");
        playButton.onClick.AddListener(PlayGame);
        exitButton.onClick.AddListener(ExitGame);
        creditsButton.onClick.AddListener(CreditsDisplay);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(creditsButton.transform.DOLocalMoveY(-250, 0.5f).SetEase(Ease.OutBounce));
        mySequence.Append(exitButton.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBounce));
        mySequence.Append(playButton.transform.DOLocalMoveY(250, 0.5f).SetEase(Ease.OutBounce));
        mySequence.Append(gameTitle1.transform.DOLocalMoveY(300, 0.5f).SetEase(Ease.OutBounce));
        mySequence.Append(gameTitle2.transform.DOLocalMoveY(300, 0.5f).SetEase(Ease.OutBounce));
        mySequence.Play();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CreditsDisplay()
    {
        creditsPanel.SetActive(true);
        creditsPanel.transform.localPosition = new Vector3(0, 1200, 0);
        creditsPanel.transform.DOLocalMoveY(0, 1f).SetEase(Ease.OutBounce).onComplete = () => { menuPanel.SetActive(false); };
    }

    public void Escape()
    {
        menuPanel.SetActive(true);
        creditsPanel.transform.DOLocalMoveY(-1200, 1f).SetEase(Ease.InBack).onComplete = () => { creditsPanel.SetActive(false); };
    }
    private void Update()
    {
        if (menuPanel.activeSelf)
        {
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Escape();
        }
    }
}
