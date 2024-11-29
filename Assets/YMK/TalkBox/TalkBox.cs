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
    [SerializeField] private RectTransform  reAskUI;
    [SerializeField] private Button         reAskBtn;
    private string talkStr;
    private IEnumerator talkEvent;
    private bool reAskEvent = false;
    private float waitTime = 0;
    private bool talkEndFlag = false;

    //되묻기 이벤트용
    private OneParaDel reAskFun;

    //단일 텍스트 이벤트용
    private NoParaDel normalFun;

    public void RunReAskText(string str, bool showReAsk, float pWaitTime, OneParaDel pFun)
    {
        talkEndFlag = false;
        reAskEvent = true;
        waitTime = pWaitTime;
        reAskUI.gameObject.SetActive(false);

        talkStr = str;
        reAskBtn.gameObject.SetActive(showReAsk);
        reAskFun = pFun;

        if (talkEvent != null)
        {
            StopCoroutine(talkEvent);
            talkEvent = null;
        }
        talkEvent = Run(talkStr);
        StartCoroutine(talkEvent);
    }

    public void RunNormalText(string str,float pWaitTime, NoParaDel pFun)
    {
        talkEndFlag = false;
        reAskEvent = false;
        waitTime = pWaitTime;
        reAskUI.gameObject.SetActive(false);

        talkStr = str;
        normalFun = pFun;

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
        if (talkEndFlag)
            return;
        talkEndFlag = true;

        if (talkEvent != null)
        {
            StopCoroutine(talkEvent);
            talkEvent = null;
        }
        talkText.text = talkStr;

        if (reAskEvent)
            reAskUI.gameObject.SetActive(true);
        else
            Invoke("NormalEventRun", waitTime);
    }

    private void NormalEventRun()
    {
        normalFun?.Invoke();
    }

    public void YesBtn()
    {
        //되묻기
        reAskFun?.Invoke(true);
    }

    public void NoBtn()
    {
        //되묻지 않음
        reAskFun?.Invoke(false);
    }
}
