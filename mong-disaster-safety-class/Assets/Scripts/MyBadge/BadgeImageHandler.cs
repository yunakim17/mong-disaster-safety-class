using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BadgeImageHandler : MonoBehaviour
{
    public Image[] badges;

    void Start()
    {
        string userId = PlayerPrefs.GetString("uuid");

        if (!string.IsNullOrEmpty(userId))
        {
            StartCoroutine(LoadBadgeImages(userId));
        }
        else
        {
            Debug.LogWarning("uuid가 존재하지 않습니다.");
        }
    }

    // 배지 이미지 경로 받아와서 보여주기
    IEnumerator LoadBadgeImages(string userId)
    {
        string url = "http://localhost:8000/stage_progress/badge_images?user_id=" + $"{userId}"; // 추후 빌드 시 url 주소 바꿔주기
        
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            // string[] 배열로 파싱
            string[] imagePaths = JsonHelper.FromJson<string>(json);

            // 각 이미지 경로 UI에 적용
            for (int i = 0; i < imagePaths.Length && i < badges.Length; i++)
            {
                Sprite sprite = Resources.Load<Sprite>(imagePaths[i]);

                if (sprite != null)
                {
                    badges[i].sprite = sprite;
                }
                else
                {
                    Debug.LogWarning($"이미지 경로 오류: {imagePaths[i]}");
                }
            }
        }
        else
        {
            Debug.LogError("서버 요청 실패: " + request.error);
        }
    }
}
