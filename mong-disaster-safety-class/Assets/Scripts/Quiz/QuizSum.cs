using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuizSum
{
    public static int correctCount = 0;

    // 정답 개수 더하기
    public static void AddCorrect()
    {
        correctCount++;
    }

    // 결과 점수 반환
    public static int GetScore()
    {
        return correctCount * 10; // 문제당 10점
    }

    // 정답 개수 리셋
    public static void Reset()
    {
        correctCount = 0;
    }
}
