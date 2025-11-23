using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class QuizMainUI : MonoBehaviour
{
    public TextMeshProUGUI scoreValue;

    void Start()
    {
        QuizSum.Reset();

        string userId = PlayerPrefs.GetString("uuid", "");

        if (string.IsNullOrEmpty(userId))
        {
            Debug.LogWarning("uuid가 존재하지 않습니다.");
            return;
        }

        StartCoroutine(LoadQuizScore(userId));
    }

    IEnumerator LoadQuizScore(string userId)
    {
        string url = $"http://3.35.180.225:8000/ranking/get/{userId}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json");

        request.certificateHandler = new BypassCertificate();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            RankingResponse data = JsonUtility.FromJson<RankingResponse>(json);
            scoreValue.text = $"{data.quiz_score}";
        }
        else
        {
            Debug.LogError("퀴즈 점수 불러오기 실패: " + request.error);
            scoreValue.text = "0";
        }
    }

    [System.Serializable]
    public class RankingResponse
    {
        public string user_id;
        public int badge_score;
        public int quiz_score;
        public int total_score;
    }
}
