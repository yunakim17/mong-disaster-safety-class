using UnityEngine;

public class Fire_Step2_S3_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step2/Fire_Step2_S3_dialogues",
            "",
            "Fire_Step2_S4");
    }
}
