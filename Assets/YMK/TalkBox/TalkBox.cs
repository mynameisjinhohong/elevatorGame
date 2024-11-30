
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    //�ǹ��� �̺�Ʈ��
    private OneParaDel                          reAskFun;

    //���� �ؽ�Ʈ �̺�Ʈ��
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
        //�ǹ���
        reAskFun?.Invoke(true);
    }

    public void NoBtn()
    {
        //�ǹ��� ����
        reAskFun?.Invoke(false);
    }
}
