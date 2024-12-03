using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI languageText;

    private void Start()
    {
        Language nowLan = LanguageMgr.GetLanguage();
        languageText.text = nowLan.ToString();
    }

    public void MoveGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ChangeLanguage()
    {
        Language nowLan = LanguageMgr.GetLanguage();
        nowLan = (Language)(((int)nowLan + 1) % (int)Language.MAX);

        LanguageMgr.SetLanguage(nowLan);
        languageText.text = nowLan.ToString();
    }
}
