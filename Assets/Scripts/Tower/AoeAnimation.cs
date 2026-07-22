using System.Collections;
using UnityEngine;

public class AoeAnimation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] bool loopAnimation;
    [SerializeField] float animationSpeed = 2;
    void OnEnable()
    {
        StartCoroutine(Animation());
    }
    void OnDisable()
    {
        StopCoroutine(Animation());
    }
    IEnumerator Animation()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        do
        {
            if (GameManager.instance.gameOver)
            {
                break;
            }
            foreach (Sprite sprite in sprites)
            {
                sr.sprite = sprite;
                yield return new WaitForSeconds(1 / animationSpeed);
            }
        }
        while (loopAnimation);
    }
}
