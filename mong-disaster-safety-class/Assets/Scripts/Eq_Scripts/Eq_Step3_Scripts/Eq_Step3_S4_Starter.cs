using UnityEngine;

public class Eq_Step3_S4_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step3/Eq_Step3_S4_dialogues",
            "Dialogues/Eq_Step3/Eq_Step3_S4_choices",
            ""
        );
    }
}
