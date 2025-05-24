using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float moveSpeed = 1f;

    void Update()
    {
        // 장애물 왼쪽으로 이동
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    // 게임 종료 시 모든 장애물 제거
    public void RemoveSelf()
    {
        Destroy(gameObject);
    }
}
