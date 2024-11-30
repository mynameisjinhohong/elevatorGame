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
        string tipFormat = string.Format("당신이 마지막으로 받은 팁 : {0:N0}$", tipValue);
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
