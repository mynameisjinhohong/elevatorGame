using System.Runtime.CompilerServices;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterObj : MonoBehaviour
{
    private NoParaDel   showFun;
    private NoParaDel   hideFun;

    [SerializeField] private TalkBox  talkBox;
    [SerializeField] private Animator characterAni;

    public bool AngryCheck = false;

    public CharacterData characterData
    {
        get;
        private set;
    }

    private int point = -1;
    public int GetPoint()
    {
        //�ش� ĳ���Ͱ� �ִ� ����

        if (characterData == null)
            return 0;

        if(point == -1)
        {
            point = UnityEngine.Random.Range(characterData.pointValue.x, characterData.pointValue.y);
        }
        return point;
    }

    private float getTime;

    public float GetPatienceTime()
    {
        //�ش� ĳ������ ���� �γ��� �ð�
        if (characterData == null)
            return 0;

        float nowTime = GameManager.instance ? GameManager.instance.time : 0;
        float time = nowTime - getTime;
        return Mathf.Max(0,characterData.maxPatience - time);
    }

    public virtual void Init(float pSpawnTime, CharacterData pCharacterData)
    {
        point = -1;
        getTime = pSpawnTime;
        showFun = null;
        hideFun = null;
        characterData = pCharacterData;

        if(talkBox != null)
            talkBox.gameObject.SetActive(false);
    }

    public void RunCharacterAction(CharacterAction pAction, NoParaDel pFun)
    {
        //ĳ���� �ִϸ��̼� �����Լ�
        talkBox.gameObject.SetActive(false);
        switch (pAction)
        {
            case CharacterAction.Show:
                {
                    characterAni.Play("Show");
                    showFun = pFun;
                }
                break;
            case CharacterAction.Hide:
                {
                    characterAni.Play("Hide");
                    hideFun = pFun;
                }
                break;
        }
    }

    public void RunCharacterActionEndEvent(CharacterAction pAction)
    {
        //ĳ���� �ִϸ��̼� ����� ����Ǵ� �Լ�
        //�ִϸ����Ϳ� �޾Ƴ��ҽ��ϴ�.
        switch (pAction)
        {
            case CharacterAction.Show:
                {
                    showFun?.Invoke();
                }
                break;
            case CharacterAction.Hide:
                {
                    hideFun?.Invoke();
                }
                break;
        }
    }

    public void RunTalkAction(int pTalkIdx, OneParaDel pFun)
    {
        //ĳ���� ��ȭ���� pTalkIdx��°�� �ش��ϴ� ��ȭ ���
        string str = string.Empty;
        switch(pTalkIdx)
        {
            case 1:
                str = characterData.talkText1;
                break;
            case 2:
                str = characterData.talkText2;
                break;
            case 3:
                str = characterData.talkText3;
                break;

        }

        talkBox.gameObject.SetActive(true);
        talkBox.RunReAskText(str, pTalkIdx != 3,0, pFun);
    }

    public void RunAngryTalkAction(NoParaDel pFun)
    {
        //ĳ���� ȭ����ȭ ���
        string str = characterData.angryText;
        SetAngry(true);
        talkBox.gameObject.SetActive(true);
        talkBox.RunNormalText(str,1f, pFun);
    }

    public void RunThankTalkAction(NoParaDel pFun)
    {
        //ĳ���� �����ȭ ���
        string str = characterData.thankText;
        SetAngry(false);
        talkBox.gameObject.SetActive(true);
        talkBox.RunNormalText(str, 1f, pFun);
    }

    protected virtual void SetAngry(bool state)
    {

    }
}
