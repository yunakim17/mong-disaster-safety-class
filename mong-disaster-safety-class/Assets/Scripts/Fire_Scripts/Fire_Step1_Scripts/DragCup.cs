using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragCup : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform matchTransform;
    public GameObject match;
    public GameObject coveredMatch;

    private Vector3 originalPosition;
    private Vector3 dragOffset;
    private bool alreadyTriggered = false; // 컵이 이미 드래그 되었는지 여부 (중복 방지)

    private RectTransform rectTransform;
    private Canvas canvas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (alreadyTriggered) return;

        originalPosition = rectTransform.anchoredPosition;
    }
    
    // 드래그 중 - 컵 위치 옮기기
    public void OnDrag(PointerEventData eventData)
    {
        if (alreadyTriggered) return;

        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pos))
        {
            rectTransform.anchoredPosition = pos;
        }
    }

    // 드래그 끝
    public void OnEndDrag(PointerEventData eventData)
    {
        if (alreadyTriggered) return;

        float distance = Vector3.Distance(rectTransform.position, matchTransform.position);

        // 일정 범위에 들어오면 이미지 변경
        if (distance < 100f)
        {
            match.SetActive(false);
            coveredMatch.SetActive(true);
            gameObject.SetActive(false);
            alreadyTriggered = true;
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
