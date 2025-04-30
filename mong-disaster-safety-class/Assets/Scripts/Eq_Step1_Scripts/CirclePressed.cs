using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;

public class circlePressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public RectTransform targetUI;
    public float shakeMagnitude = 10f;

    private bool isHolding = false;
    private Coroutine shakeCoroutine;
    private Vector3 originalPos;

    void Start()
    {
        if (targetUI != null)
            originalPos = targetUI.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;

        if (shakeCoroutine == null)
            shakeCoroutine = StartCoroutine(ShakeUI());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;

        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            shakeCoroutine = null;
        }

        if (targetUI != null)
            targetUI.anchoredPosition = originalPos;
    }

    System.Collections.IEnumerator ShakeUI()
    {
        while (isHolding)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            targetUI.anchoredPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            yield return new WaitForSeconds(0.05f); // 흔들림 간격 (천천히 흔들리게)
        }

        // 손을 뗐을 때 복원 (혹시 누락될 경우 대비)
        if (targetUI != null)
            targetUI.anchoredPosition = originalPos;
    }
}
