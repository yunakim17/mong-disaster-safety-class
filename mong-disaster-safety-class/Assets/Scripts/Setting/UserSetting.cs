using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;

public class UserSetting : MonoBehaviour
{
    // InputField
    public TMP_InputField schoolInput;
    public TMP_InputField nicknameInput;

    // Dropdown
    public TMP_Dropdown ageDropdown;

    // Button
    public Button checkButton;
    public Button editButton;

    // Warning Text
    public GameObject schoolWarning;
    public TMP_Text schoolWarningText;
    public GameObject nicknameWarning;
    public TMP_Text nicknameWarningText;

    public SchoolType schoolTypeSelector;

    public Color activeColor = new Color32(142, 211, 206, 255);
    public Color inactiveColor = new Color32(180, 180, 180, 255);

    // 제어 변수
    private bool isNicknameValid = false;
    private bool isNicknameAvailable = false;
    private bool isSchoolValid = false;
    private bool isAnyFieldChanged = false;

    private string userId;

    public GameObject checkPanel;

    void Start()
    {
        // 수정 버튼 비활성화
        editButton.interactable = false;
        editButton.image.color = inactiveColor;

        // 기존 유저 데이터 불러오기
        userId = PlayerPrefs.GetString("uuid");
        StartCoroutine(LoadUserData(userId));

        // 이벤트 리스너 연결
        schoolInput.onValueChanged.AddListener(OnSchoolChanged);
        nicknameInput.onValueChanged.AddListener(OnNicknameChanged);
        ageDropdown.onValueChanged.AddListener(delegate { OnAnyFieldChanged(); });

        checkButton.onClick.AddListener(() =>
        {
            StartCoroutine(CheckNickname());
        });

        editButton.onClick.AddListener(() =>
        {
            StartCoroutine(UpdateUser());
        });
    }

    // 기존 유저 데이터 불러오기
    IEnumerator LoadUserData(string userId)
    {
        string url = "http://3.35.180.225:8000/user/" + UnityWebRequest.EscapeURL(userId);

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.certificateHandler = new BypassCertificate();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            UserData data = JsonUtility.FromJson<UserDataWrapper>("{\"data\":" + json + "}").data;

            // School Type Button 상태 설정
            if (data.school_name.Contains("초등학교"))
            {
                schoolTypeSelector.SetSchoolType("초등학교");
            }
            else if (data.school_name.Contains("유치원"))
            {
                schoolTypeSelector.SetSchoolType("유치원");
            }

            // Input Field 설정
            schoolInput.text = data.school_name;
            ageDropdown.value = data.age - 3;
            nicknameInput.text = data.nickname;

            isNicknameValid = true;
            isNicknameAvailable = true;
            isSchoolValid = true;
            isAnyFieldChanged = false;

            UpdateEditButtonState();
        }
        else
        {
            Debug.LogError("유저 정보 불러오기 실패: " + request.error);
        }
    }

    // 닉네임 변경 감지
    public void OnNicknameChanged(string newText)
    {
        isAnyFieldChanged = true;
        isNicknameAvailable = false;

        if (string.IsNullOrWhiteSpace(nicknameInput.text))
        {
            nicknameWarning.SetActive(true);
            nicknameWarningText.text = "닉네임을 입력해주세요!";
            nicknameWarningText.color = Color.red;
            isNicknameValid = false;
        }
        else if (nicknameInput.text.Length > 10)
        {
            nicknameWarning.SetActive(true);
            nicknameWarningText.text = "닉네임은 10글자까지 입력할 수 있어요!";
            nicknameWarningText.color = Color.red;
            isNicknameValid = false;
        }
        else
        {
            nicknameWarning.SetActive(false);
            isNicknameValid = true;
        }

        UpdateEditButtonState();
    }

    // 학교명 변경 감지
    public void OnSchoolChanged(string newText)
    {
        isAnyFieldChanged = true;

        isNicknameAvailable = false;
        nicknameWarning.SetActive(false);

        if (string.IsNullOrWhiteSpace(newText))
        {
            schoolWarning.SetActive(true);
            schoolWarningText.text = "학교 이름을 입력해주세요!";
            schoolWarningText.color = Color.red;
            isSchoolValid = false;
        }
        else if (newText.Length > 20)
        {
            schoolWarning.SetActive(true);
            schoolWarningText.text = "학교 이름은 20글자까지 입력할 수 있어요!";
            schoolWarningText.color = Color.red;
            isSchoolValid = false;
        }
        else
        {
            schoolWarning.SetActive(false);
            isSchoolValid = true;
        }

        UpdateEditButtonState();
    }

    // 나이 변경 감지
    public void OnAnyFieldChanged()
    {
        isAnyFieldChanged = true;
        UpdateEditButtonState();
    }

    // 닉네임 중복 검사
    IEnumerator CheckNickname()
    {
        string nickname = nicknameInput.text.Trim();

        if (string.IsNullOrEmpty(nickname))
        {
            nicknameWarning.SetActive(true);
            nicknameWarningText.text = "닉네임을 입력해주세요!";
            nicknameWarningText.color = Color.red;

            isNicknameAvailable = false;
            UpdateEditButtonState();
            yield break;
        }

        string schoolName = NormalizeSchoolName();

        string url = "http://3.35.180.225:8000/user/check-nickname?nickname=" + UnityWebRequest.EscapeURL(nickname)
            + "&school_name=" + UnityWebRequest.EscapeURL(schoolName);
        
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.certificateHandler = new BypassCertificate();
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            bool exists = request.downloadHandler.text.Contains("true");

            nicknameWarning.SetActive(true);

            if (exists)
            {
                nicknameWarningText.text = "이미 사용 중인 닉네임입니다.";
                nicknameWarningText.color = Color.red;

                isNicknameAvailable = false;
            }
            else
            {
                nicknameWarningText.text = "사용 가능한 닉네임입니다!";
                nicknameWarningText.color = Color.blue;

                isNicknameAvailable = true;
            }

            UpdateEditButtonState();
        }
    }

    // 수정 완료 버튼 활성화
    void UpdateEditButtonState()
    {
        if (isNicknameValid && isNicknameAvailable && isSchoolValid && isAnyFieldChanged)
        {
            editButton.interactable = true;
            editButton.image.color = activeColor;
        }
        else
        {
            editButton.interactable = false;
            editButton.image.color = inactiveColor;
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

    // 유저 정보 수정하기
    IEnumerator UpdateUser()
    {
        string schoolName = NormalizeSchoolName();

        // 서버에 보낼 데이터
        var data = new UserData
        {
            user_id = userId,
            school_name = schoolName,
            nickname = nicknameInput.text,
            age = ageDropdown.value + 3
        };

        string json = JsonUtility.ToJson(data);
        string url = "http://3.35.180.225:8000/user/update";

        UnityWebRequest request = new UnityWebRequest(url, "PUT");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.certificateHandler = new BypassCertificate();

        yield return request.SendWebRequest();

        // 수정 성공
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("유저 정보 수정 성공");
            isAnyFieldChanged = false;
            UpdateEditButtonState();

            checkPanel.SetActive(true);
        }
        // 수정 실패
        else
        {
            Debug.LogError("유저 정보 수정 실패: " + request.downloadHandler.text);
        }
    }

    // 수정 완료 패널 확인 버튼 클릭 시 메인 씬으로 전환
    public void OnClickCheckPanel()
    {
        this.GetComponent<SceneChanger>().Main();
    }

    [System.Serializable]
    public class UserData
    {
        public string user_id;
        public string school_name;
        public int age;
        public string nickname;
    }

    [System.Serializable]
    public class UserDataWrapper
    {
        public UserData data;
    }

    public class BypassCertificate : CertificateHandler
    {
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}
