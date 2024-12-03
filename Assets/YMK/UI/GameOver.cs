using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Animation ani;
    [SerializeField] private TextMeshProUGUI tipText;

    public void RunGameOver()
    {
        gameObject.SetActive(true);
        int tipValue = GameManager.instance ? GameManager.instance.tip : 0;
        string strFormat = LanguageMgr.GetText("GameOver_1");
        string tipFormat = string.Format(strFormat, tipValue);
        tipText.text = tipFormat;
        ani.Play();
    }

    public void ReStartGame()
    {
        //게임 재시작처리
        CharacterMgr.ClearAll();
        SceneManager.LoadScene("GameScene");
    }

}
