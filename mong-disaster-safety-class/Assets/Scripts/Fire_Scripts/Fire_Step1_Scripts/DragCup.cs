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
    public GameObject nextButton;

    private Vector3 originalPosition;
    private Vector3 dragOffset;
    private bool alreadyTriggered = false; // ���� �̹� �巡�� �Ǿ����� ���� (�ߺ� ����)

    private RectTransform rectTransform;
    private Canvas canvas;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    // �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (alreadyTriggered) return;

        originalPosition = rectTransform.anchoredPosition;
    }
    
    // �巡�� �� - �� ��ġ �ű��
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

    // �巡�� ��
    public void OnEndDrag(PointerEventData eventData)
    {
        if (alreadyTriggered) return;

        float distance = Vector3.Distance(rectTransform.position, matchTransform.position);

        // ���� ������ ������ �̹��� ����
        if (distance < 100f)
        {
            match.SetActive(false);
            coveredMatch.SetActive(true);
            gameObject.SetActive(false);
            alreadyTriggered = true;
            nextButton.SetActive(true);
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
