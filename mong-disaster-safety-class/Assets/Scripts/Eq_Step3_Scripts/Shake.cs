using UnityEngine;

public class Shake : MonoBehaviour
{
    private Vector3 originalPosition;
    private float shakeIntensity = 0f;
    private float fadeOutSpeed = 1f;
    private bool isShaking = false;
    private bool isFading = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (isShaking)
        {
            Vector3 offset = Random.insideUnitSphere * shakeIntensity * 0.3f;
            offset.z = 0f; // 2D에서 Z축 흔들림 방지
            transform.localPosition = originalPosition + offset;

            if (isFading)
            {
                shakeIntensity = Mathf.MoveTowards(shakeIntensity, 0f, fadeOutSpeed * Time.deltaTime * 0.3f);
                if (shakeIntensity <= 0.00001f)
                {
                    isShaking = false;
                    transform.localPosition = originalPosition;
                }
            }
        }
    }

    // 흔들기 시작
    public void StartShake(float intensity)
    {
        originalPosition = transform.localPosition;
        shakeIntensity = intensity;
        isShaking = true;
        isFading = false;
    }

    // 부드럽게 멈추기
    public void StopShake(float fadeTime = 1f)
    {
        fadeOutSpeed = fadeTime > 0f ? 1f / fadeTime : 999f;
        isFading = true;
    }
}
