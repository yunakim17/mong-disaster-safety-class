using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class UserDeleteHandler : MonoBehaviour
{
    private string userId;

    public GameObject checkPanel;
    public GameObject deleteCheckPanel;
    public TMP_Text deleteCheckText;

    private bool isDeleteSuccess = false;

    void Start()
    {
        userId = PlayerPrefs.GetString("uuid");
    }
    
    // '네' 버튼 클릭 시 유저 삭제 진행
    public void OnClickYesButton()
    {
        StartCoroutine(DeleteUser(userId));
    }

    // 유저 삭제 (Cascade)
    IEnumerator DeleteUser(string userId)
    {
        string url = $"http://3.35.180.225:8000/user/users/{userId}";
        UnityWebRequest request = UnityWebRequest.Delete(url);
        request.certificateHandler = new BypassCertificate();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            isDeleteSuccess = true;
            deleteCheckText.text = "정보가 지워졌습니다!";
            checkPanel.SetActive(false);
            deleteCheckPanel.SetActive(true);
            checkPanel.SetActive(false);
        }
        else
        {
            isDeleteSuccess = false;
            deleteCheckText.text = $"정보가 지워지지 않았습니다: \n{request.error}";
            checkPanel.SetActive(false);
            deleteCheckPanel.SetActive(true);
        }
    }

    // 결과 확인 버튼 클릭 시 회원가입 씬으로 이동
    public void OnClickDeleteCheckButton()
    {
        // 유저 삭제 성공
        if (isDeleteSuccess)
        {
            this.GetComponent<SceneChanger>().Sign_Up();
        }
        // 유저 삭제 실패
        else
        {
            deleteCheckPanel.SetActive(false);
        }

    }

    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
