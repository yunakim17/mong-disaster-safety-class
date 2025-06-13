using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class QuizResult : MonoBehaviour
{
    private string userId;
    private int stageId;

    // UI
    public TextMeshProUGUI thisScoreValue;      // 받은 점수
    public TextMeshProUGUI questionValue;       // 전체 문항 수
    public TextMeshProUGUI correctValue;        // 맞힌 문항 수
    public TextMeshProUGUI wholeScoreValue;     // 누적 점수

    void Start()
    {
        userId = PlayerPrefs.GetString("uuid", "");
        stageId = PlayerPrefs.GetInt("stage_id_quiz", -1);

        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("uuid가 존재하지 않습니다.");
            return;
        }

        if (stageId == -1)
        {
            Debug.LogWarning("stage_id가 설정되어 있지 않습니다.");
            return;
        }

        int resultScore = QuizSum.GetScore();
        StartCoroutine(SendScore(userId, stageId, resultScore));
        PlayerPrefs.DeleteKey("stage_id_quiz");
    }

    IEnumerator SendScore(string userId, int stageId, int score)
    {
        string url = $"http://3.35.180.225:8000/stage_progress/update_score" +
            $"?user_id={userId}&stage_id={stageId}";
        string rawScore = score.ToString();

        UnityWebRequest request = UnityWebRequest.Put(url, rawScore);
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(rawScore));
        request.downloadHandler = new DownloadHandlerBuffer();

        request.certificateHandler = new BypassCertificate();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("퀴즈 점수 전송 성공");

            StartCoroutine(UpdateRanking(userId));
        }
        else
        {
            Debug.LogError("퀴즈 점수 전송 실패: " + request.error);
        }
    }

    IEnumerator UpdateRanking(string userId)
    {
        string url = $"http://3.35.180.225:8000/ranking/update-quiz/{userId}";

        UnityWebRequest request = UnityWebRequest.Put(url, "");
        request.SetRequestHeader("Content-Type", "application/json");

        request.certificateHandler = new BypassCertificate();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("퀴즈 점수 업데이트 성공");

            string json = request.downloadHandler.text;
            Debug.Log("서버 응답 JSON: " + json);

            RankingResponse data = JsonUtility.FromJson<RankingResponse>(json);

            UpdateUI(data.quiz_score);
            QuizSum.Reset();
        }
        else
        {
            Debug.LogError("퀴즈 점수 업데이트 실패: " + request.error);
        }
    }

    // UI 업데이트
    public void UpdateUI(int totalScore)
    {
        int correct = QuizSum.correctCount;
        int total = PlayerPrefs.GetInt("quiz_total", 0);
        int score = correct * 10;

        thisScoreValue.text = $"{score}";
        questionValue.text = $"{total}";
        correctValue.text = $"{correct}";
        wholeScoreValue.text = $"{totalScore}";
    }

    // JSON 파싱용 클래스
    [System.Serializable]
    public class RankingResponse
    {
        public string status;
        public string user_id;
        public int quiz_score;
        public int badge_score;
        public int total_score;
    }
}