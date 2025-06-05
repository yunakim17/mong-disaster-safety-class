using UnityEngine;

public class Fire_Step3_S1_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject speaker;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step3/Fire_Step3_S1_dialogues",
            "",
            "Fire_Step3_S2");
    }

    void Update()
    {
        int currentIdx = dialogueManager.GetCurrentLineIndex();

        // 3번째 대사(index = 2)에서 이미지 활성화
        if (currentIdx == 2)
        {
            speaker?.SetActive(true);
        }
    }
}
