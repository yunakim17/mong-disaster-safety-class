using UnityEngine;

public class Fe_Step1_S4_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step1/Fire_Step1_S4_dialogues",
            "",
            "Fire_Step1_S5");
    }
}
