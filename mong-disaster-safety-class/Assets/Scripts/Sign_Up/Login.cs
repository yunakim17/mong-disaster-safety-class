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
        // �׽�Ʈ��: ����� uuid ����
        PlayerPrefs.DeleteKey("uuid");

        // ���� ��ư ��Ȱ��ȭ
        startButton.interactable = false;
        startButton.image.color = inactiveColor;

        // UUID�� �̹� ������ �ڵ� �α��� �� �ٷ� �������� �̵�
        if (PlayerPrefs.HasKey("uuid"))
        {
            Debug.Log("�α��� ����");
            this.GetComponent<SceneChanger>().Main();
            return;
        }

        // ������ �� UUID ����
        string uuid = Guid.NewGuid().ToString();
        PlayerPrefs.SetString("uuid", uuid);
        PlayerPrefs.Save();
    }

    // �ߺ� Ȯ�� ��ư
    public void OnClickCheckButton()
    {
        StartCoroutine(CheckNickname());
    }

    // ���� ���� ��ư
    public void OnClickStartButton()
    {
        bool isValid = true;

        // �б� �Է� Ȯ��
        if (string.IsNullOrWhiteSpace(schoolInput.text))
        {
            schoolWarning.SetActive(true);
            isValid = false;
        }
        else
        {
            schoolWarning.SetActive(false);
        }

        // ������ ȸ������ ��û
        if (isValid)
        {
            StartCoroutine(RegisterUser());
        }
    }

    // �г��� �ߺ� �˻�(/check-nickname)
    IEnumerator CheckNickname()
    {
        string nickname = nicknameInput.text.Trim();

        if (string.IsNullOrEmpty(nickname))
        {
            nicknameWarning.SetActive(true);
            nicknameWarningText.text = "�г����� �Է����ּ���!";
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
                nicknameWarningText.text = "�̹� ��� ���� �г����Դϴ�.";
                nicknameWarningText.color = Color.red;
                startButton.interactable = false;
                startButton.image.color = inactiveColor;
            }
            else
            {
                nicknameWarningText.text = "��� ������ �г����Դϴ�!";
                nicknameWarningText.color = Color.blue;
                startButton.interactable = true;
                startButton.image.color = activeColor;
            }
        }
    }

    // ���� ���(/register)
    IEnumerator RegisterUser()
    {
        string uuid = PlayerPrefs.GetString("uuid");
        string schoolName = NormalizeSchoolName();

        // ������ ���� ������
        var data = new RegisterData
        {
            user_id = uuid,
            school_name = schoolName,
            nickname = nicknameInput.text,
            age = ageDropdown.value + 3
        };

        string json = JsonUtility.ToJson(data);
        string url = "http://localhost:8000/register"; // ���� ���� �� url �ּ� �ٲ��ֱ�
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // ȸ������ ���� -> ���� ������ ��ȯ
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("ȸ������ ����");
            this.GetComponent<SceneChanger>().Main();
        }
        // ȸ������ ����
        else
        {
            // �г��� �ߺ�
            if (request.responseCode == 409)
            {
                //nicknameWarning.SetActive(true);
                Debug.LogWarning("�̹� �����ϴ� �г����Դϴ�.");
            }
            else
            {
                Debug.LogError("ȸ������ ����: " + request.downloadHandler.text);
            }
        }    
    }

    // �б� �̸� ���͸�
    string NormalizeSchoolName()
    {
        string name = schoolInput.text.Trim();
        string type = schoolTypeSelector.GetSelectedSchoolType();

        if (type == "�ʵ��б�")
        {
            name = name.Replace("�ʵ��б�", "");
            return name + "�ʵ��б�";
        }

        if (type == "��ġ��")
        {
            name = name.Replace("��ġ��", "");
            return name + "��ġ��";
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
