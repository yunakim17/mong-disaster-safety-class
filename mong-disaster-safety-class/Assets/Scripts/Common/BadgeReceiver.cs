using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BadgeReceiver : MonoBehaviour
{
    public int stageId;
    public bool isCompleted = false;

    // 서버에 전송할 데이터 형식 정의
    [System.Serializable]
    public class StageProgressData
    {
        public string user_id;  // 유저 ID
        public int stage_id;    // 스테이지 ID
        public bool completed;  // 완료 여부
    }

    // 스테이지 ID를 PlayerPrefs에 저장
    void Awake()
    {
        PlayerPrefs.SetInt("stage_id", stageId);
    }

    void Start()
    {
        string userId = PlayerPrefs.GetString("uuid", "default_user");
        int stageId = PlayerPrefs.GetInt("stage_id", -1);

        if (userId != "default_user" && stageId != -1)
        {
            CompleteStage(userId, stageId);
        }
        else
        {
            Debug.LogWarning("user_id 또는 stage_id가 저장되어 있지 않습니다.");
        }
    }

    void Update()
    {
        // 화면 클릭 시 메인 씬으로 이동
        if (isCompleted && Input.GetMouseButtonDown(0))
        {
            GetComponent<SceneChanger>().Main();
        }
    }

    // 데이터를 서버에 전송
    public void CompleteStage(string userId, int stageId)
    {
        StageProgressData data = new StageProgressData
        {
            user_id = userId,
            stage_id = stageId,
            completed = true
        };

        StartCoroutine(SendStageProgress(data));
    }

    // 서버에 PUT 요청 보내기
    IEnumerator SendStageProgress(StageProgressData requestData)
    {
        string url = "http://localhost:8000/stage_progress/complete" +
            $"?user_id={requestData.user_id}&stage_id={requestData.stage_id}&" +
            $"completed={requestData.completed.ToString().ToLower()}"; // 추후 빌드 시 url 주소 바꿔주기

        UnityWebRequest request = UnityWebRequest.Put(url, "");
        request.method = UnityWebRequest.kHttpVerbPUT;
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // 디버깅용
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("스테이지 완료 처리 성공");
            isCompleted = true;
        }
        else
        {
            Debug.Log("스테이지 완료 처리 실패: " + request.error);
        }
    }
}
