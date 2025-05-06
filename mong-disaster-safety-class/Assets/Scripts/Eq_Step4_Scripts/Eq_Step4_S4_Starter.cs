using UnityEngine;

public class Eq_Step4_S4_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step4/Eq_Step4_S4_dialogues",
            "",
            ""
        );
    }
}
