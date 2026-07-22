using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator transition;
    public float transitionWait = 1;
    public void LevelLoad()
    {
        StartCoroutine( LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        //play animation
        transition.SetTrigger("transition");

        //wait
        yield return new WaitForSeconds(transitionWait);

        //load level
        SceneManager.LoadScene(LevelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
