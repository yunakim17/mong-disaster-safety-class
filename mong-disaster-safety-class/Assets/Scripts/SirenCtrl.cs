using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SirenCtrl : MonoBehaviour
{
    public Vector3 targetScale = new Vector3(1f, 1f, 1f); // Ȯ�� ũ��
    public float duration = 0.5f; // Ȯ��/��� �� �ֱ� �ð� (1��)

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

    // �ܺο��� ȣ���Ͽ� �ݺ� ����
    public void StartPulse()
    {
        isPulsing = true;
        timer = 0f;
    }

    // �ʿ�� �ݺ� ����
    public void StopPulse()
    {
        isPulsing = false;
        transform.localScale = originalScale;
    }
}
