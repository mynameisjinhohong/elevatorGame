using System.Runtime.CompilerServices;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterObj : MonoBehaviour
{
    private NoParaDel   showFun;
    private NoParaDel   hideFun;
    private OneParaDel  talkFun;

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
    }

}
