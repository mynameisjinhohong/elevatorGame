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
    [SerializeField] private TextMeshProUGUI    talkText;
    [SerializeField] private float              textDelay;
    [SerializeField] private RectTransform      reAskUI;
    [SerializeField] private Button             reAskBtn;
    private string                              talkStr;
    private bool                                reAskEvent = false;
    private float                               waitTime = 0;
    private bool                                talkEndFlag = false;
    private float                               speakDelay = 0.6f;
    private IEnumerator                         talkEvent;
    private SFX                                 loopSFX;

    //되묻기 이벤트용
    private OneParaDel                          reAskFun;

    //단일 텍스트 이벤트용
    private NoParaDel                           normalFun;

    public void RunReAskText(string str, bool showReAsk, float pWaitTime, SFX pSFX, OneParaDel pFun)
    {
        talkEndFlag = false;
        reAskEvent = true;
        talkStr = str;
        waitTime = pWaitTime;
        loopSFX = pSFX;
        reAskFun = pFun;

        reAskUI.gameObject.SetActive(false);
        reAskBtn.gameObject.SetActive(showReAsk);

        AudioManager audioManager = GameManager.instance?.audioManager;
        if(audioManager != null)
            audioManager.StartAudioLoop(pSFX);

        if (talkEvent != null)
        {
            StopCoroutine(talkEvent);
            talkEvent = null;
        }
        talkEvent = Run(talkStr);

        StartCoroutine(talkEvent);
    }

    public void RunNormalText(string str,float pWaitTime, SFX pSFX, NoParaDel pFun)
    {
        talkEndFlag = false;
        reAskEvent = false;
        talkStr = str;
        waitTime = pWaitTime;
        loopSFX = pSFX;
        normalFun = pFun;
        reAskUI.gameObject.SetActive(false);

        AudioManager audioManager = GameManager.instance?.audioManager;
        if (audioManager != null)
            audioManager.StartAudio(pSFX);

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
        float speakTime = speakDelay;
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

        AudioManager audioManager = GameManager.instance?.audioManager;
        if (audioManager != null)
            audioManager.StopAudio(loopSFX);

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
