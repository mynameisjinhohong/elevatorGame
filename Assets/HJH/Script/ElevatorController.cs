using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ElevatorController : MonoBehaviour
{
    public Vector3 origionPos_R;
    public Vector3 originPos_L;
    public Transform right;
    public Transform left;
    public float duration = 1f;
    public Transform endPoint_r;
    public Transform endPoint_l; 

    public Ease ease;

    void Awake(){
        origionPos_R = right.localPosition;
        originPos_L = left.localPosition;
    }

    [Button]
    public void OpenElevator()
    {
        //���������� �����°� �ִϸ��̼� ����
        //���������� �� ������ ���� GameManager.instance.EndOpenElevator() ȣ��.
        left.DOLocalMove(endPoint_l.localPosition,duration,false).SetEase(ease);
        right.DOLocalMove(endPoint_r.localPosition,duration,false).SetEase(ease).OnComplete(()=>{GameManager.instance.EndOpenElevator();});
    }
    
    [Button]
    public void CloseElevator()
    {
        left.DOLocalMove(originPos_L,duration,false).SetEase(ease);
        right.DOLocalMove(origionPos_R,duration,false).SetEase(ease).OnComplete(()=>{GameManager.instance.EndCloseElevator();});
        //���������� �����°� �ִϸ��̼� ����
        //���������� ������ ���� GameManager.instance.EndCloseElevator() ȣ��
        
    }

}
