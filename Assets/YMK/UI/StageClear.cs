using TMPro;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    private NoParaDel aniEndFun;
    [SerializeField] private Animation ani;
    [SerializeField] private TextMeshProUGUI tipText;

    public void RunStageClear(NoParaDel pFun)
    {
        aniEndFun = pFun;
        int tipValue = GameManager.instance ? GameManager.instance.tip : 0;
        string strFormat = LanguageMgr.GetText("StageClear_2");
        string tipFormat = string.Format(strFormat, tipValue);
        tipText.text = tipFormat;
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
