using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchoolType : MonoBehaviour
{
    public Button schoolButton;
    public Button kindergartenButton;

    public Color32 selectedColor = new Color32(255, 130, 130, 255);
    public Color32 defaultColor = new Color32(249, 221, 105, 255);

    public string selectedSchoolType = "초등학교"; // 선택한 학교 타입

    void Start()
    {
        SetSchoolType("초등학교");
    }

    public void OnClickSchool()
    {
        SetSchoolType("초등학교");
    }

    public void OnClickKindergarten()
    {
        SetSchoolType("유치원");
    }

    // 선택된 버튼의 색상 변경
    public void SetSchoolType(string type)
    {
        selectedSchoolType = type;

        if (type == "초등학교")
        {
            schoolButton.image.color = selectedColor;
            kindergartenButton.image.color = defaultColor;
        }
        else
        {
            kindergartenButton.image.color = selectedColor;
            schoolButton.image.color = defaultColor;
        }
    }

    // 선택된 버튼 값 getter
    public string GetSelectedSchoolType()
    {
        return selectedSchoolType;
    }
}
