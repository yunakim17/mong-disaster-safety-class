using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public DistanceBar distanceBar;

    private bool isInvincible = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 장애물 충돌 처리
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("obstacle") && !isInvincible)
        {
            // 진행 바 색 변경
            if (distanceBar != null)
                distanceBar.ChangeFillColor(distanceBar.hitColor);

            // 속도 늦춤
            Time.timeScale = 0.8f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            StartCoroutine(InvincibleRoutine());
        }
    }

    // 깜빡임 + 무적 상태
    IEnumerator InvincibleRoutine()
    {
        isInvincible = true;

        float blinkTime = 0.1f;
        int blinkCount = 10;

        for (int i = 0; i < blinkCount; i++)
        {
            // 투명도 낮춤
            Color c = spriteRenderer.color;
            c.a = 0.3f;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(blinkTime);

            // 투명도 다시 높임
            c.a = 1f;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(blinkTime);
        }

        isInvincible = false;

        // 속도 복구
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // 진행 바 색상 복구
        if (distanceBar != null)
            distanceBar.ChangeFillColor(distanceBar.normalColor);
    }
}
