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

    public void GoNextStage()
    {
        CharacterMgr.ClearAll();
        gameObject.SetActive(false);

        aniEndFun?.Invoke();
    }
}
