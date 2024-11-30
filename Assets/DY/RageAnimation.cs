using UnityEngine;
using DG.Tweening;
public class RageAnimation : MonoBehaviour
{
    public RectTransform characterRecttransform; // 움직일 캐릭터 RectTransform
    public int bounceCount = 5; // 몇 번 반사할지 설정
    public float moveDuration = 1.0f; // 이동 시간 (초 단위)

    private RectTransform canvasRectTransform;
    private Vector2 originalPosition; // 캐릭터의 초기 위치

    private void Start()
    {
        // Canvas의 RectTransform을 가져와 화면의 경계를 계산
        canvasRectTransform = characterRecttransform.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        originalPosition = characterRecttransform.anchoredPosition; // 시작 위치 저장
        StartBouncing();
    }

    private void StartBouncing()
    {
        // 첫 번째 이동 시작
        MoveToNextEdge(bounceCount);
    }

    private void MoveToNextEdge(int remainingBounces)
    {
        if (remainingBounces <= 0)
        {
            // 반사 이동이 끝나면 원래 위치로 돌아가기
            ReturnToOriginalPosition();
            return;
        }

        Vector2 currentPosition = characterRecttransform.anchoredPosition;
        Vector2 targetPosition = GetReflectedPoint(currentPosition); // 반사 위치 계산

        // DOTween을 사용해 캐릭터 이동
        characterRecttransform
            .DOAnchorPos(targetPosition, moveDuration)
            .SetEase(Ease.Linear) // 선형 이동
            .OnComplete(() =>
            {
                // 이동 완료 후 다음 이동 호출
                MoveToNextEdge(remainingBounces - 1);
            });
    }

    private void ReturnToOriginalPosition()
    {
        // DOTween을 사용해 원래 위치로 이동
        characterRecttransform
            .DOAnchorPos(originalPosition, moveDuration)
            .SetEase(Ease.InOutQuad);
    }

    private Vector2 GetReflectedPoint(Vector2 currentPosition)
    {
        // Canvas 크기 가져오기
        float screenWidth = canvasRectTransform.rect.width;
        float screenHeight = canvasRectTransform.rect.height;

        Vector2 reflectedPoint = Vector2.zero;

        // 현재 위치에 따라 반사 위치 계산
        if (currentPosition.y >= screenHeight) // 위쪽 변
        {
            reflectedPoint = new Vector2(Random.Range(0, screenWidth), 0); // 아래쪽 변으로 이동
        }
        else if (currentPosition.y <= 0) // 아래쪽 변
        {
            reflectedPoint = new Vector2(Random.Range(0, screenWidth), screenHeight); // 위쪽 변으로 이동
        }
        else if (currentPosition.x >= screenWidth) // 오른쪽 변
        {
            reflectedPoint = new Vector2(0, Random.Range(0, screenHeight)); // 왼쪽 변으로 이동
        }
        else if (currentPosition.x <= 0) // 왼쪽 변
        {
            reflectedPoint = new Vector2(screenWidth, Random.Range(0, screenHeight)); // 오른쪽 변으로 이동
        }

        return reflectedPoint;
}
}
