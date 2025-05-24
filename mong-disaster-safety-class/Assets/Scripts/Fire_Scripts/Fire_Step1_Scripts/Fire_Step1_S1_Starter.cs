using UnityEngine;

public class Fire_Step1_S1_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step1/Fire_Step1_S1_dialogues",
            "",
            "Fire_Step1_S2");
    }
}
