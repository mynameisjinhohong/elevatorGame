using UnityEngine;

public class CharacterObj : MonoBehaviour
{
    private NoParaDel   showFun;
    private NoParaDel   hideFun;

    [SerializeField] private TalkBox  talkBox;
    [SerializeField] private Animator characterAni;

    [System.NonSerialized] public bool angryCheck = false;

    public CharacterData characterData
    {
        get;
        private set;
    }

    public int GetPoint()
    {
        //해당 캐릭터가 주는 점수

        if (characterData == null)
            return 0;

        return characterData.pointValue;
    }

    private float getTime;

    public float GetPatienceTime()
    {
        //해당 캐릭터의 남은 인내심 시간
        if (characterData == null)
            return 0;

        float nowTime = GameManager.instance ? GameManager.instance.time : 0;
        float time = nowTime - getTime;
        return Mathf.Max(0,characterData.maxPatience - time);
    }

    public virtual void Init(float pSpawnTime, CharacterData pCharacterData)
    {
        getTime = pSpawnTime;
        showFun = null;
        hideFun = null;
        characterData = pCharacterData;
        angryCheck= false;
        if(talkBox != null)
            talkBox.gameObject.SetActive(false);
    }

    public void RunCharacterAction(CharacterAction pAction, NoParaDel pFun)
    {
        //캐릭터 애니메이션 실행함수
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
        //캐릭터 애니메이션 종료시 실행되는 함수
        //애니메이터에 달아놓았습니다.
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
        //캐릭터 대화실행 pTalkIdx번째에 해당하는 대화 출력
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

        bool showReAsk = pTalkIdx != 3;
        talkBox.gameObject.SetActive(true);

        SFX[] sfxArray = new SFX[] { SFX.nomalChat1, SFX.nomalChat2, SFX.nomalChat3 };
        int rIdx = UnityEngine.Random.Range(0, sfxArray.Length);
        SFX randomSFX = sfxArray[rIdx];

        talkBox.RunReAskText(str, showReAsk, 0, randomSFX, pFun);
    }

    public void RunAngryTalkAction(NoParaDel pFun)
    {
        //캐릭터 화난대화 출력
        string str = characterData.angryText;
        SetAngry(true);
        talkBox.gameObject.SetActive(true);

        SFX[] sfxArray = new SFX[] { SFX.angryChat1, SFX.angryChat2, SFX.angryChat3 };
        int rIdx = UnityEngine.Random.Range(0, sfxArray.Length);
        SFX randomSFX = sfxArray[rIdx];

        talkBox.RunNormalText(str,1f, randomSFX,pFun);
    }

    public void RunThankTalkAction(NoParaDel pFun)
    {
        //캐릭터 감사대화 출력
        string str = characterData.thankText;
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
