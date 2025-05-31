using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    [Header("Shake Settings")]
    public float magnitude = 0.2f; 
    public float interval = 0.03f;

    private Vector3 originalPosition;

    void Awake()
    {
        Instance = this;
        originalPosition = transform.localPosition;
    }

    void Start()
    {
        InvokeRepeating(nameof(ShakeOnce), 0f, interval);
    }

    void ShakeOnce()
    {
        float x = Random.Range(-1f, 1f) * magnitude;
        float y = Random.Range(-1f, 1f) * magnitude;

        transform.localPosition = originalPosition + new Vector3(x, y, 0);
    }

    void OnDisable()
    {
        CancelInvoke();
        transform.localPosition = originalPosition;
    }
}
