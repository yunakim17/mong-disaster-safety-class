using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Warning Text
    public GameObject schoolWarning;
    public GameObject nicknameWarning;

    // 입력값 검증
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

        // 닉네임 입력 확인
        if (string.IsNullOrWhiteSpace(nicknameInput.text))
        {
            nicknameWarning.SetActive(true);
            isValid = false;
        }
        else
        {
            nicknameWarning.SetActive(false);
        }

        if (isValid)
        {
            this.GetComponent<SceneChanger>().Main(); // 씬 전환
        }
    }
}
