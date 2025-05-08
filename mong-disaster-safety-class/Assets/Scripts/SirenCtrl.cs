using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenCtrl : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(1f, 1f, 1f); // 확대 크기
    public float duration = 0.5f; // 확대/축소 한 주기 시간 (1초)

    private Vector3 originalScale;
    private bool isPulsing = false;
    private float timer = 0f;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isPulsing)
        {
            timer += Time.deltaTime;
            float t = Mathf.PingPong(timer, duration) / duration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
        }
    }

    // 외부에서 호출하여 반복 시작
    public void StartPulse()
    {
        isPulsing = true;
        timer = 0f;
    }

    // 필요시 반복 중지
    public void StopPulse()
    {
        isPulsing = false;
        transform.localScale = originalScale;
    }
}
