using UnityEngine;

public class Eq_Step1_S4_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step1/Eq_Step1_S4_dialogues",
            "",
            "Eq_Step1_Badge");
    }
}
