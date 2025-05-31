using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject introUI;
    public Button startButton;

    public GameObject obstacleSpawner;
    public GameObject distanceBar;

    public GameObject clearUI;

    void Start()
    {
        introUI.SetActive(true);

        obstacleSpawner.SetActive(false);
        distanceBar.SetActive(false);

        startButton.onClick.AddListener(StartGame);
    }

    /* 게임 시작 시
     * - 인트로 UI 비활성화
     * - 장애물 스포너 & 거리 바 활성화
     */
    void StartGame()
    {
        introUI.SetActive(false);

        obstacleSpawner.SetActive(true);
        distanceBar.SetActive(true);
    }

    // 게임 종료 시 클리어 UI 활성화
    public void ShowClearText()
    {
        clearUI.SetActive(true);
    }
}