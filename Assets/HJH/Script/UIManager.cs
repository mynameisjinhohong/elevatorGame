using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject elevatorDown;
    public GameObject[] floorIconPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFloorButton(int idx)
    {
        floorIconPos[idx].transform.GetChild(idx).gameObject.SetActive(true);
    }

    public void OffFloorButton(int idx)
    {
        floorIconPos[idx].transform.GetChild(idx).gameObject.SetActive(false);
    }
}
