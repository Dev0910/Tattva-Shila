using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Tutorial : MonoBehaviour
{

    public GameObject[] Slides;
    private int currentIndex = 0;
    public Animator panelAnimator;
    bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadNextSlide()
    {
        if (!isOpen)
        {
            StartCoroutine(SlideShow());
        }
    }

    IEnumerator SlideShow()
    {
        isOpen = true;
        if (currentIndex < Slides.Length - 1)
        {
            panelAnimator.SetTrigger("transition");
            yield return new WaitForSeconds(2f);
            Slides[currentIndex].gameObject.SetActive(false);
            currentIndex++;
            panelAnimator.SetTrigger("BackIn");
            Slides[currentIndex].gameObject.SetActive(true);

        }

        else
        {
            panelAnimator.SetTrigger("transition");
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        isOpen = false;
    }
}
