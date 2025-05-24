using UnityEngine;

public class Eq_Step4_S1_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step4/Eq_Step4_S1_dialogues",
            "Dialogues/Eq_Step4/Eq_Step4_S1_choices",
            "Eq_Step4_S2"
        );
    }
}
