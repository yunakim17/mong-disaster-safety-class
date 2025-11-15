using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public AudioSource hitSound; // 효과음
    public float fadeDuration = 0.5f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        // 효과음 재생
        if (hitSound != null)
            hitSound.Play();

        StartCoroutine(FadeAndDestroy());
    }

    // 장애물 천천히 사라지기
    IEnumerator FadeAndDestroy()
    {
        float elapsed = 0f;
        Color originalColor = spriteRenderer.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }
}
