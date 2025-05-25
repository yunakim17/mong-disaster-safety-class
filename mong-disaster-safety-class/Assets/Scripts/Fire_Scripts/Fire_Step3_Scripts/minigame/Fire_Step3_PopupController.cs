using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Fire_Step3_PopupController : MonoBehaviour
{
    public GameObject popupPanel;
    public TMP_Text popupText;
    public AudioSource audioSource;
    public AudioClip successClip;
    public AudioClip uselessClip;

    public bool IsPopupActive { get; private set; }

    void Start()
    {
        popupPanel.SetActive(false);
    }

    public void ShowPopup(string message)
    {
        if (IsPopupActive) return;

        IsPopupActive = true;
        popupText.text = message;
        popupPanel.SetActive(true);

        audioSource.clip = message.Contains("Ã£¾Ò´Ù") ? successClip : uselessClip;
        audioSource.Play();

        StartCoroutine(WaitAudioEnd());
    }

    IEnumerator WaitAudioEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        popupPanel.SetActive(false);
        IsPopupActive = false;
    }
}
