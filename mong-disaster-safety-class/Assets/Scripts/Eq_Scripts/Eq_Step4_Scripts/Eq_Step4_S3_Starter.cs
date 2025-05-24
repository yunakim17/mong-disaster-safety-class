using UnityEngine;

public class Eq_Step4_S3_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step4/Eq_Step4_S3_dialogues",
            "Dialogues/Eq_Step4/Eq_Step4_S3_choices",
            "Eq_Step4_S4"
        );
    }
}
