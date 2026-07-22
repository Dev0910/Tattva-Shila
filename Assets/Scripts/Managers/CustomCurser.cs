using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCurser : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }
    public void SetSprite(Sprite _sprite)
    {
        GetComponent<SpriteRenderer>().sprite = _sprite;
    }
}
