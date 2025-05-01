using UnityEngine;

public class Eq_Step2_S1_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S1_dialogues",
            "",
            "Eq_Step2_S2");
    }
}
