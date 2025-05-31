using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void Update()
    {
        // 장애물이 화면 밖으로 넘어가면 제거
        if (transform.position.x < -12)
        {
            Destroy(gameObject);
        }
    }
}
