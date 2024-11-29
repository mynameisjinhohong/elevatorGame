using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image clockUI;
    public TMP_Text hpText;
    public GameObject mapCanvas;
    public Image hpBG;
    public Sprite[] hpBGSprite;
    public int[] hpInts;
    public TMP_Text memberText;
    public TMP_Text tipText;

    public GameObject[] arrow;
    public GameObject[] lamp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clockUI.fillAmount = 1 - GameManager.instance.time / GameManager.instance.maxTime;
        if(GameManager.instance.hp < hpInts[0])
        {
            hpBG.sprite = hpBGSprite[1];
        }
        else if(GameManager.instance.hp < hpInts[1])
        {
            hpBG.sprite = hpBGSprite[2];
        }
        else
        {
            hpBG.sprite = hpBGSprite[0];
        }
        hpText.text = GameManager.instance.hp.ToString() + "%";
        memberText.text = GameManager.instance.nowElevatorCharacter.Count + "/4";
        tipText.text = GameManager.instance.tip.ToString() + "$";
    }

    


    public void OnFloorArrowButton(int idx)
    {
        if (!arrow[idx].activeInHierarchy)
        {
            arrow[idx].SetActive(true);
        }
    }

    public void OffFloorButton(int idx)
    {
        if (arrow[idx].activeInHierarchy)
        {
            arrow[idx].SetActive(false);
        }
    }

    public void TurnOnElevatorButton()
    {

    }
    public void TurnOffElevatorButton()
    {

    }

    public void OpenMap()
    {
        mapCanvas.SetActive(true);
    }

    public void CloseMap()
    {
        mapCanvas.SetActive(false);
    }

    public void LampOn(int idx)
    {
        for(int i = 0; i<lamp.Length; i++)
        {
            if(idx == i)
            {
                lamp[i].SetActive(true);
            }
            else
            {
                lamp[i].SetActive(false);
            }
        }
    }
}
