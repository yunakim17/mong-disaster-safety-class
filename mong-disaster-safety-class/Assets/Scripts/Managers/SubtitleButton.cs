using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleButton : MonoBehaviour
{
    public Button toggleButton;
    public TMP_Text buttonText;

    void Start()
    {
        toggleButton.onClick.AddListener(OnToggleSubtitle);
        UpdateButtonText();
    }

    void OnToggleSubtitle()
    {
        SubtitleManager.isSubtitleOn = !SubtitleManager.isSubtitleOn;
        UpdateButtonText();
    }

    void UpdateButtonText()
    {
        if (SubtitleManager.isSubtitleOn == true)
        {
            buttonText.text = "끄기";
            toggleButton.image.color = HexToColor("#FF997D");
        }

        else
        {
            buttonText.text = "켜기";
            toggleButton.image.color = HexToColor("#75E768");
        }
    }

    Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
            return color;

        return Color.white; // 실패 시 기본값
    }
}
