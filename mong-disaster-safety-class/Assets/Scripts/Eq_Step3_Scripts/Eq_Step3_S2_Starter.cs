using UnityEngine;

public class Eq_Step3_S2_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step3/Eq_Step3_S2_dialogues",
            "",
            "Eq_Step3_S3"
        );
    }
}
