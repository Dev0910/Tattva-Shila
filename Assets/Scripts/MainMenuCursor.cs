using UnityEngine;

public class MainMenuCursor : MonoBehaviour
{
    private RectTransform rectTransform;

    void Awake()
    {
        // Cache the RectTransform for UI positioning
        rectTransform = GetComponent<RectTransform>();
        Cursor.visible = false;
    }

    void Update()
    {
        // Update cursor position to follow mouse in screen space
        rectTransform.position = Input.mousePosition;
    }
}
