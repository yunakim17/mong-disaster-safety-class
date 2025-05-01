using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed;
    public MeshRenderer meshRenderer;

    private bool isScrolling = true;

    void Update()
    {
        // 게임 종료 시 스크롤 중단
        if (!isScrolling) return;

        // 배경 자동 스크롤
        meshRenderer.material.mainTextureOffset += new Vector2(scrollSpeed * Time.deltaTime, 0);
    }

    public void StopScrolling()
    {
        isScrolling = false;
    }
}
