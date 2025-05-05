using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;

public class Login : MonoBehaviour
{
    // InputField
    public TMP_InputField schoolInput;
    public TMP_InputField nicknameInput;

    // Dropdown
    public TMP_Dropdown ageDropdown;

    // Image
    public Image schoolLabel;
    public Image ageLabel;
    public Image nicknameLabel;
    public Image ageLabel2;

    // Button
    public Button schoolButton;
    public Button kindergartenButton;
    public Button checkButton;
    public Button startButton;

    public Color activeColor = new Color32(142, 211, 206, 255);
    public Color inactiveColor = new Color32(180, 180, 180, 255);

    // Warning Text
    public GameObject schoolWarning;
    public GameObject nicknameWarning;
    public TMP_Text nicknameWarningText;

    public SchoolType schoolTypeSelector;

    void Start()
    {
        // 테스트용: 저장된 uuid 삭제
        PlayerPrefs.DeleteKey("uuid");

        // 시작 버튼 비활성화
        startButton.interactable = false;
        startButton.image.color = inactiveColor;

        // UUID가 이미 있으면 자동 로그인 → 바로 메인으로 이동
        if (PlayerPrefs.HasKey("uuid"))
        {
            Debug.Log("로그인 성공");
            this.GetComponent<SceneChanger>().Main();
            return;
        }

        // 없으면 새 UUID 생성
        string uuid = Guid.NewGuid().ToString();
        PlayerPrefs.SetString("uuid", uuid);
        PlayerPrefs.Save();
    }

    // 중복 확인 버튼
    public void OnClickCheckButton()
    {
        StartCoroutine(CheckNickname());
    }

    // 게임 시작 버튼
    public void OnClickStartButton()
    {
        bool isValid = true;

        // 학교 입력 확인
        if (string.IsNullOrWhiteSpace(schoolInput.text))
        {
            schoolWarning.SetActive(true);
            isValid = false;
        }
        else
        {
            schoolWarning.SetActive(false);
        }

        // 서버로 회원가입 요청
        if (isValid)
        {
            StartCoroutine(RegisterUser());
        }
    }

    // 닉네임 중복 검사(/check-nickname)
    IEnumerator CheckNickname()
    {
        string nickname = nicknameInput.text.Trim();

        if (string.IsNullOrEmpty(nickname))
        {
            nicknameWarning.SetActive(true);
            nicknameWarningText.text = "닉네임을 입력해주세요!";
            nicknameWarningText.color = Color.red;
            startButton.interactable = false;
            startButton.image.color = inactiveColor;
            yield break;
        }

        string url = "http://localhost:8000/check-nickname?nickname=" + UnityWebRequest.EscapeURL(nickname);
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            bool exists = request.downloadHandler.text.Contains("true");

            nicknameWarning.SetActive(true);

            if (exists)
            {
                nicknameWarningText.text = "이미 사용 중인 닉네임입니다.";
                nicknameWarningText.color = Color.red;
                startButton.interactable = false;
                startButton.image.color = inactiveColor;
            }
            else
            {
                nicknameWarningText.text = "사용 가능한 닉네임입니다!";
                nicknameWarningText.color = Color.blue;
                startButton.interactable = true;
                startButton.image.color = activeColor;
            }
        }
    }

    // 유저 등록(/register)
    IEnumerator RegisterUser()
    {
        string uuid = PlayerPrefs.GetString("uuid");
        string schoolName = NormalizeSchoolName();

        // 서버에 보낼 데이터
        var data = new RegisterData
        {
            user_id = uuid,
            school_name = schoolName,
            nickname = nicknameInput.text,
            age = ageDropdown.value + 3
        };

        string json = JsonUtility.ToJson(data);
        string url = "http://localhost:8000/register"; // 추후 빌드 시 url 주소 바꿔주기
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // 회원가입 성공 -> 메인 씬으로 전환
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("회원가입 성공");
            this.GetComponent<SceneChanger>().Main();
        }
        // 회원가입 실패
        else
        {
            // 닉네임 중복
            if (request.responseCode == 409)
            {
                //nicknameWarning.SetActive(true);
                Debug.LogWarning("이미 존재하는 닉네임입니다.");
            }
            else
            {
                Debug.LogError("회원가입 실패: " + request.downloadHandler.text);
            }
        }    
    }

    // 학교 이름 필터링
    string NormalizeSchoolName()
    {
        string name = schoolInput.text.Trim();
        string type = schoolTypeSelector.GetSelectedSchoolType();

        if (type == "초등학교")
        {
            name = name.Replace("초등학교", "");
            return name + "초등학교";
        }

        if (type == "유치원")
        {
            name = name.Replace("유치원", "");
            return name + "유치원";
        }

        return name + type;
    }


    [Serializable]
    public class RegisterData
    {
        public string user_id;
        public string school_name;
        public int age;
        public string nickname;
    }
}
