using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float longPressDuration = 3f; // 롱터치로 인식되는 시간
    public Vector3 enlargedScale = new Vector3(2.5f, 2.5f, 1f); // 커질 크기

    private Vector3 originalScale;
    private float pressTime;
    private bool isPointerDown = false;
    private bool isScaling = false;
    private bool hasLongPressed = false;

    public SirenCtrl SirenCtrl;

    void Start()
    {
        originalScale = transform.localScale;
     
    }

    void Update()
    {
        if (isPointerDown)
        {
            pressTime += Time.deltaTime;
            float t = Mathf.Clamp01(pressTime / longPressDuration);
            transform.localScale = Vector3.Lerp(originalScale, enlargedScale, t);

            if (pressTime >= 2f && !hasLongPressed)
            {
                hasLongPressed = true; // 한 번만 실행되게
                SirenCtrl.StartPulse(); // 여기서 바로 실행됩니다
            }
        }

        if (!isPointerDown && isScaling)
        {
            // 손을 뗀 후 다시 원래 크기로 서서히 되돌림
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.localScale, originalScale) < 0.01f)
            {
                transform.localScale = originalScale;
                isScaling = false;
            }

            hasLongPressed = false; // 한 번만 실행되게
            SirenCtrl.StopPulse();

        }

       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        pressTime = 0f;
        isScaling = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        isScaling = true;
        hasLongPressed = false;
    }
}
