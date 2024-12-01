using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SafeAreaZone : MonoBehaviour
{
    [SerializeField] private RectTransform rectBox0;
    [SerializeField] private RectTransform rectBox1;
    [SerializeField] private RectTransform rectBox2;
    [SerializeField] private RectTransform rectBox3;

    private void Awake()
    {
        SetZone();
    }

#if UNITY_EDITOR
    private void Update()
    {
        SetZone();
    }
#endif

    private void SetZone()
    {
        Rect safeArea = Screen.safeArea;
        Vector2 minAnchor = safeArea.position;
        Vector2 maxAnchor = minAnchor + safeArea.size;
        Vector2 newMinPos = minAnchor;
        Vector2 newMaxPos = maxAnchor;

        if (safeArea.width * SafeArea.SCREEN_HEIGHT < SafeArea.SCREEN_WIDTH * safeArea.height)
        {
            float newHeight = safeArea.width * (SafeArea.SCREEN_HEIGHT / SafeArea.SCREEN_WIDTH);

            float minX = minAnchor.x;
            float minY = minAnchor.y + (safeArea.height - newHeight) / 2.0f;

            float maxX = maxAnchor.x;
            float maxY = maxAnchor.y - (safeArea.height - newHeight) / 2.0f;

            newMinPos = new Vector2(minX, minY);
            newMaxPos = new Vector2(maxX, maxY);
        }
        else
        {
            float newWidth = safeArea.height * (SafeArea.SCREEN_WIDTH / SafeArea.SCREEN_HEIGHT);

            float minY = minAnchor.y;
            float minX = minAnchor.x + (safeArea.width - newWidth) / 2.0f;

            float maxY = maxAnchor.y;
            float maxX = maxAnchor.x - (safeArea.width - newWidth) / 2.0f;

            newMinPos = new Vector2(minX, minY);
            newMaxPos = new Vector2(maxX, maxY);
        }

        newMinPos.x /= Mathf.Max(0.001f, (float)Screen.width);
        newMinPos.y /= Mathf.Max(0.001f, (float)Screen.height);
        newMaxPos.x /= Mathf.Max(0.001f, (float)Screen.width);
        newMaxPos.y /= Mathf.Max(0.001f, (float)Screen.height);

        if (rectBox0 != null && rectBox1 != null)
        {
            rectBox0.transform.localScale = new Vector3(newMinPos.x, 1, 1);
            rectBox1.transform.localScale = new Vector3(1.0f - newMaxPos.x, 1, 1);
        }
        if (rectBox2 != null && rectBox3 != null)
        {
            rectBox2.transform.localScale = new Vector3(1, newMinPos.y, 1);
            rectBox3.transform.localScale = new Vector3(1, 1.0f - newMaxPos.y, 1);
        }
    }
}
