using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class BadgeScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreValue;
    private string userId;

    // 랭킹 데이터
    [System.Serializable]
    public class RankingData
    {
        public string user_id;
        public int badge_score;
        public int quiz_score;
        public int total_score;
    }

    void Start()
    {
        userId = PlayerPrefs.GetString("uuid");

        if (!string.IsNullOrEmpty(userId))
        {
            StartCoroutine(FetchBadgeScore(userId));
        }
        else
        {
            Debug.LogWarning("uuid가 존재하지 않습니다.");
        }
    }

    // 배지 점수 불러오기
    IEnumerator FetchBadgeScore(string userId)
    {
        string url = $"https://3.35.180.225:8000/ranking/get/{userId}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.certificateHandler = new BypassCertificate();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            RankingData data = JsonUtility.FromJson<RankingData>(json);
            scoreValue.text = data.badge_score.ToString();
        }
        else
        {
            Debug.LogError("배지 점수 불러오기 실패: " + request.error);
        }
    }
}
