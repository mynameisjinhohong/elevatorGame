using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetMoney : MonoBehaviour
{
    [SerializeField] private Animation ani;
    [SerializeField] private TextMeshProUGUI getTipText;

    public void RunGetMoney(int pGetPoint)
    {
        gameObject.SetActive(true);
        string tipFormat = string.Format("{0:N0}$", pGetPoint);
        getTipText.text = tipFormat;
        ani.Play();
    }

    public void MoneyAniEnd()
    {
        //애니메이션 종료시
        gameObject.SetActive(false);
    }

}
