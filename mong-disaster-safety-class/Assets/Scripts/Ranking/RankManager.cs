using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

// 랭킹 데이터
[System.Serializable]
public class RankData
{
    public int rank;
    public string nickname;
    public int age;
    public int total_score;
    public string user_id;
}

// 학교 이름 데이터
[System.Serializable]
public class SchoolData
{
    public string school_name;
}

// 서버에서 받아올 랭킹 리스트 데이터
[System.Serializable]
public class RankListData
{
    public string school_name;
    public List<RankData> ranking;
}

public class RankManager : MonoBehaviour
{
    public TextMeshProUGUI schoolName;
    public GameObject rankPrefab;
    public Transform contentTransform;

    void Start()
    {
        StartCoroutine(GetRankData());
    }

    // 랭킹 데이터 가져오기
    IEnumerator GetRankData()
    {
        string userId = PlayerPrefs.GetString("uuid");

        // 학교 이름 요청
        string userUrl = "http://localhost:8000/user/school/" + userId; // 추후 빌드 시 url 주소 바꿔주기
        UnityWebRequest userRequest = UnityWebRequest.Get(userUrl);
        yield return userRequest.SendWebRequest();

        if (userRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("학교 이름 요청 실패: " + userRequest.error);
            yield break;
        }

        // 학교 이름으로 랭킹 리스트 요청
        string userJson = userRequest.downloadHandler.text;
        SchoolData schoolData = JsonUtility.FromJson<SchoolData>(userJson);
        string schoolName = schoolData.school_name;

        string rankUrl = "http://localhost:8000/ranking/list/" + schoolName;
        UnityWebRequest rankRequest = UnityWebRequest.Get(rankUrl);
        yield return rankRequest.SendWebRequest();

        if (rankRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("랭킹 요청 실패: " + rankRequest.error);
            yield break;
        }

        string rankJson = rankRequest.downloadHandler.text;
        ProcessRankData(rankJson);
    }

    // 랭킹 리스트 데이터 파싱
    void ProcessRankData(string json)
    {
        RankListData rankListData = JsonUtility.FromJson<RankListData>(json);
        Debug.Log("학교 이름: " + rankListData.school_name);

        schoolName.text = rankListData.school_name;
        
        foreach (var data in rankListData.ranking)
        {
            Debug.Log($"{data.rank}위: {data.nickname} - {data.total_score}점");
            GameObject newRank = Instantiate(rankPrefab, contentTransform);
            RankSetting rankSetting = newRank.GetComponent<RankSetting>();
            rankSetting.Setup(data.rank, data.nickname, data.age, data.total_score, data.user_id);
        }
    }
}
