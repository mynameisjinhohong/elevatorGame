using TMPro;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    private NoParaDel aniEndFun;
    [SerializeField] private Animation ani;

    public void RunStageClear(NoParaDel pFun)
    {
        aniEndFun = pFun;

        gameObject.SetActive(true);
        ani.Play();
    }

    public void RunEndAniEvent()
    {
        CharacterMgr.ClearAll();
        aniEndFun?.Invoke();
    }
}
