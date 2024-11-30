using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    [SerializeField] private Animation ani;
    [SerializeField] private TextMeshProUGUI tipText;

    public void RunGameClear()
    {
        gameObject.SetActive(true);
        int tipValue = GameManager.instance ? GameManager.instance.tip : 0;
        string tipFormat = string.Format("당신이 모은 팁 : {0:N0}$", tipValue);
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
