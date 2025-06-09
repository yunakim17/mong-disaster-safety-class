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
    }

    private IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueTextObject.SetActive(false);
    }
}