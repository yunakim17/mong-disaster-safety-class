using UnityEngine;

public class Eq_Step3_S3_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "",
            "",
            "Eq_Step3_S4"
        );
    }
}
