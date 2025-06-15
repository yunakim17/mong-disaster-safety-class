using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // 중력 제거
    }

    void Update()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    public void MoveUp() => moveDirection = Vector2.up;
    public void MoveDown() => moveDirection = Vector2.down;
    public void MoveLeft() => moveDirection = Vector2.left;
    public void MoveRight() => moveDirection = Vector2.right;
    public void Stop() => moveDirection = Vector2.zero;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌 감지: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("벽에 닿음");
            moveDirection = Vector2.zero;
        }

        if (collision.gameObject.CompareTag("Fire"))
        {
            Debug.Log("불에 닿음! 게임 오버!");
            moveDirection = Vector2.zero;
            Invoke("RestartGame", 0.3f);
        }
    }

    private void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
