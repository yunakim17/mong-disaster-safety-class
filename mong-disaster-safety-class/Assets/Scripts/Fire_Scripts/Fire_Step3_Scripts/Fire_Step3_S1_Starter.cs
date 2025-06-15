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
            "Fire_Step3_S3");
    }

    void Update()
    {
        int currentIdx = dialogueManager.GetCurrentLineIndex();

        //인덱스 2일떄 스피커 이미지 
        if (currentIdx == 1)
        {
            speaker?.SetActive(true);
        }
        if (currentIdx == 3)
        {
            speaker?.SetActive(false);
        }
    }
}
