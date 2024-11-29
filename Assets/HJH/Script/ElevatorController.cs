using System.Collections;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public GameObject right;
    public GameObject left;
    public float speed;
    public float endPoint;
    public void OpenElevator()
    {
        //엘리베이터 열리는거 애니메이션 구현
        //엘리베이터 다 열리고 나서 GameManager.instance.EndOpenElevator() 호출.
    }

    public void CloseElevator()
    {
        //엘리베이터 닫히는거 애니메이션 구현
        //엘리베이터 닫히고 나서 GameManager.instance.EndCloseElevator() 호출
    }

}
