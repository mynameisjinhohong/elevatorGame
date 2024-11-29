using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image clockUI;
    public TMP_Text hpText;
    public GameObject elevatorDown;
    public GameObject[] floorIconPos;
    public GameObject mapCanvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        clockUI.fillAmount = 1 - GameManager.instance.time / GameManager.instance.maxTime;
        hpText.text = GameManager.instance.hp.ToString() + "%";
    }


    public void OnFloorButton(int idx)
    {
        if (!floorIconPos[idx].transform.GetChild(idx).gameObject.activeInHierarchy)
        {
            floorIconPos[idx].transform.GetChild(idx).gameObject.SetActive(true);
        }
    }

    public void OffFloorButton(int idx)
    {
        if (floorIconPos[idx].transform.GetChild(idx).gameObject.activeInHierarchy)
        {
            floorIconPos[idx].transform.GetChild(idx).gameObject.SetActive(false);
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
}
