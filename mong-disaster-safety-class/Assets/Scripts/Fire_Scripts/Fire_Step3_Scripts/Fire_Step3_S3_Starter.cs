using UnityEngine;

public class Fire_Step3_S3_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step3/Fire_Step3_S3_dialogues",
            "Dialogues/Fire_Step3/Fire_Step3_S3_choices",
            "Fire_Step3_S4");
    }
}
