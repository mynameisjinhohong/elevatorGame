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
        //���������� �����°� �ִϸ��̼� ����
        //���������� �� ������ ���� GameManager.instance.EndOpenElevator() ȣ��.
    }

    public void CloseElevator()
    {
        //���������� �����°� �ִϸ��̼� ����
        //���������� ������ ���� GameManager.instance.EndCloseElevator() ȣ��
    }

}
