using UnityEngine;
using System.Collections;

public class Fire_Step3_S5_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager; // 기존 자막 시스템
    public GameObject dialogueTextObject;

    void Start()
    {
        dialogueTextObject.SetActive(true);
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step3/Fire_Step3_S5_dialogues", 
            "", 
            "");

        StartCoroutine(HideTextAfterDelay(10f));     
        Debug.Log("BGMManager 인스턴스: " + fire_step3_BGM.Instance);
        Debug.Log("오디오 소스 존재? " + fire_step3_BGM.Instance.bgmSource);
        Debug.Log("현재 clip: " + fire_step3_BGM.Instance.bgmSource.clip);
        Debug.Log("isPlaying: " + fire_step3_BGM.Instance.bgmSource.isPlaying);
        Debug.Log("mute: " + fire_step3_BGM.Instance.bgmSource.mute);
        Debug.Log("volume: " + fire_step3_BGM.Instance.bgmSource.volume); 
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueTextObject.SetActive(false);
    }
}