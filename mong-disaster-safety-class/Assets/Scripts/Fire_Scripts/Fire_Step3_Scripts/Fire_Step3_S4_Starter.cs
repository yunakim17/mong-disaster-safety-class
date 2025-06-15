using UnityEngine;
using System.Collections;

public class Fire_Step3_S4_Starter : MonoBehaviour
{
    public GameObject targetPanel; // 감지 대상 패널
    public DialogueManager dialogueManager; // 기존 자막 시스템
    public GameObject dialogueTextObject; // 자막 텍스트 UI 오브젝트

    private bool hasTriggered = false;

    void Update()
    {
        if (targetPanel.activeSelf && !hasTriggered)
        {
            hasTriggered = true;

            // 자막 텍스트 오브젝트 활성화
            dialogueTextObject.SetActive(true);

            // 자막 시작
            dialogueManager.StartDialogue("Dialogues/Fire_Step3/Fire_Step3_S4_dialogues", "", "");

            // 2초 뒤 자막 텍스트 숨기기
            StartCoroutine(HideTextAfterDelay(5f));
        }
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueTextObject.SetActive(false);
    }
}

