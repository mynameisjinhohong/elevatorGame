using System.Runtime.CompilerServices;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterObj : MonoBehaviour
{
    private NoParaDel   showFun;
    private NoParaDel   hideFun;

    [SerializeField] private TalkBox  talkBox;
    [SerializeField] private Animator characterAni;

    public CharacterData characterData
    {
        get;
        private set;
    }

    private int point = -1;
    public int GetPoint()
    {
        //해당 캐릭터가 주는 점수

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
        //해당 캐릭터의 남은 인내심 시간
        if (characterData == null)
            return 0;

        float time = GameManager.instance.time - getTime;
        return Mathf.Max(0,characterData.maxPatience - time);
    }

    public virtual void Init(float pSpawnTime)
    {
        point = -1;
        getTime = pSpawnTime;
        showFun = null;
        hideFun = null;
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

        talkBox.gameObject.SetActive(true);
        talkBox.RunText(str, pFun);
    }
}
