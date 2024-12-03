using UnityEngine;

public class CharacterObj : MonoBehaviour
{
    private NoParaDel   showFun;
    private NoParaDel   hideFun;
    private NoParaDel   getOutFun;
    private NoParaDel   spawnFun;

    [SerializeField] private TalkBox  talkBox;
    [SerializeField] private Animator characterAni;

    [System.NonSerialized] public bool angryCheck = false;
    [System.NonSerialized] public bool completeCheck = false;

    public CharacterData characterData
    {
        get;
        private set;
    }

    public int GetPoint()
    {
        //�ش� ĳ���Ͱ� �ִ� ����

        if (characterData == null)
            return 0;

        return characterData.pointValue;
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
        getTime         = pSpawnTime;
        showFun         = null;
        hideFun         = null;
        characterData   = pCharacterData;
        angryCheck      = false;
        completeCheck   = false;
        if (talkBox != null)
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
            case CharacterAction.GetOut:
                {
                    characterAni.Play("GetOut");
                    getOutFun = pFun;
                }
                break;
            case CharacterAction.Spawn:
                {
                    characterAni.Play("Spawn");
                    spawnFun = pFun;
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
            case CharacterAction.GetOut:
                {
                    getOutFun?.Invoke();
                }
                break;
            case CharacterAction.Spawn:
                {
                    spawnFun?.Invoke();
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
                str = LanguageMgr.GetText(characterData.talkKey1);
                break;
            case 2:
                str = LanguageMgr.GetText(characterData.talkKey2);
                break;
            case 3:
                str = LanguageMgr.GetText(characterData.talkKey3);
                break;

        }

        bool showReAsk = pTalkIdx != 3;
        talkBox.gameObject.SetActive(true);

        SFX[] sfxArray = new SFX[] { SFX.nomalChat1, SFX.nomalChat2, SFX.nomalChat3 };
        int rIdx = UnityEngine.Random.Range(0, sfxArray.Length);
        SFX randomSFX = sfxArray[rIdx];

        talkBox.RunReAskText(str, showReAsk, 0, randomSFX, pFun);
    }

    public void RunAngryTalkAction(NoParaDel pFun)
    {
        //ĳ���� ȭ����ȭ ���
        string str = LanguageMgr.GetText(characterData.angryKey);
        SetAngry(true);
        talkBox.gameObject.SetActive(true);

        SFX[] sfxArray = new SFX[] { SFX.angryChat1, SFX.angryChat2, SFX.angryChat3 };
        int rIdx = UnityEngine.Random.Range(0, sfxArray.Length);
        SFX randomSFX = sfxArray[rIdx];

        talkBox.RunNormalText(str,1f, randomSFX,pFun);
    }

    public void RunThankTalkAction(NoParaDel pFun)
    {
        //ĳ���� �����ȭ ���
        string str = LanguageMgr.GetText(characterData.thankKey);
        SetAngry(false);
        talkBox.gameObject.SetActive(true);

        SFX[] sfxArray = new SFX[] { SFX.goodChat1, SFX.goodChat2, SFX.goodChat3 };
        int rIdx = UnityEngine.Random.Range(0, sfxArray.Length);
        SFX randomSFX = sfxArray[rIdx];

        talkBox.RunNormalText(str, 1f, randomSFX, pFun);
    }

    protected virtual void SetAngry(bool state)
    {

    }
}
