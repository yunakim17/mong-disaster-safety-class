using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDelete : MonoBehaviour
{
    public GameObject checkPanel;

    // ÆÐ³Î ¶ç¿ì±â
    public void ShowCheckPanel()
    {
        checkPanel.SetActive(true);
    }

    public void HideCheckPanel()
    {
        checkPanel.SetActive(false);
    }
}
