using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.Rendering;

public class TalkBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI talkText;
    [SerializeField] private float textDelay;
    [SerializeField] private RectTransform reAskUI;
    private string talkStr;
    private IEnumerator talkEvent;

    //
    private OneParaDel fun;

    public void RunText(string str, OneParaDel pFun)
    {
        reAskUI.gameObject.SetActive(false);

        talkStr = str;
        fun = pFun;
        if (talkEvent != null)
        {
            StopCoroutine(talkEvent);
            talkEvent = null;
        }
        talkEvent = Run(talkStr);
        StartCoroutine(talkEvent);
    }

    private IEnumerator Run(string str)
    {
        string textStr = string.Empty;
        for (int i = 0; i < str.Length; i++)
        {
            textStr += str[i];
            talkText.text = textStr;
            yield return new WaitForSeconds(textDelay);
        }
        talkText.text = str;
        TalkEnd();
    }

    public void TalkEnd()
    {
        if (talkEvent != null)
        {
            StopCoroutine(talkEvent);
            talkEvent = null;
        }
        talkText.text = talkStr;
        reAskUI.gameObject.SetActive(true);
    }

    public void YesBtn()
    {
        //µÇ¹¯±â
        fun?.Invoke(true);
    }

    public void NoBtn()
    {
        //µÇ¹¯Áö ¾ÊÀ½
        fun?.Invoke(false);
    }
}
