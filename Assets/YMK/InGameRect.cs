using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class InGameRect : MonoBehaviour
{
    private RectTransform rectTrans = null;

    private void Awake()
    {
        SetScale();
    }

#if UNITY_EDITOR
    private void Update()
    {
        SetScale();
    }
#endif

    private void SetScale()
    {
        //ȭ�� ������ ����ؼ�
        //canvas�� ũ�⸦ �����Ѵ�.
        rectTrans ??= GetComponent<RectTransform>();
        //rectTrans.sizeDelta = new Vector2(SafeArea.SCREEN_WIDTH, SafeArea.SCREEN_HEIGHT);
        SafeArea.SetSafeArea(rectTrans);
    }
}
