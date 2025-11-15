using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public DistanceBar distanceBar;

<<<<<<< Updated upstream
    private bool isGameEnded = false;

    public Animator PlayerAnimator;
=======
    private bool isInvincible = false;
>>>>>>> Stashed changes

    public AudioSource jumpSound;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

<<<<<<< Updated upstream
    void Update()
    {
        // 게임 종료 시 점프 x
        if (isGameEnded) return;

        bool isTouch = false;

        // 화면 터치 감지 (모바일용)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            isTouch = true;

        // 마우스 클릭 감지 (컴퓨터 테스트용)
        if (Input.GetMouseButtonDown(0))
            isTouch = true;

        // 플레이어 점프
        if (isTouch && isGrounded)
        {
            PlayerRigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            PlayerAnimator.SetInteger("state", 1);
            jumpSound.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            if (!isGrounded)
                PlayerAnimator.SetInteger("state", 2);

            isGrounded = true;
        }
    }

    // 장애물 충돌 시
=======
    // 장애물 충돌 처리
>>>>>>> Stashed changes
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

    // 깜빡임 + 무적 상태 설정
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
            yield return new WaitForSecondsRealtime(blinkTime);

            // 투명도 다시 높임
            c.a = 1f;
            spriteRenderer.color = c;
            yield return new WaitForSecondsRealtime(blinkTime);
        }

        isInvincible = false;

        // 정상 속도 & 슬라이더 색상 복구
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        if (distanceBar != null)
            distanceBar.ChangeFillColor(distanceBar.normalColor);
    }
}
