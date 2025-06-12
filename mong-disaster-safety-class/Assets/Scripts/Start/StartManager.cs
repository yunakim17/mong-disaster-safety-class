using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    void Awake()
    {
        // uuid 삭제 (개발용)
        //PlayerPrefs.DeleteKey("uuid");

        // 퀴즈 정답 개수 초기화
        QuizSum.Reset();
    }

    void Start()
    {
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1f); // 분기 전 1초 대기

        // UUID 존재 여부 확인
        if (PlayerPrefs.HasKey("uuid"))
        {
            // 자동 로그인: 메인으로 이동
            GetComponent<SceneChanger>().Main();
        }
        else
        {
            // UUID 없음: 로그인 화면으로 이동
            GetComponent<SceneChanger>().Sign_Up();
        }
    }
}
