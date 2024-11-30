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
        string tipFormat = string.Format("����� ���� �� : {0:N0}$", tipValue);
        tipText.text = tipFormat;
        ani.Play();
    }

    public void ReStartGame()
    {
        //���� �����ó��
        CharacterMgr.ClearAll();
        SceneManager.LoadScene("GameScene");
    }

}
