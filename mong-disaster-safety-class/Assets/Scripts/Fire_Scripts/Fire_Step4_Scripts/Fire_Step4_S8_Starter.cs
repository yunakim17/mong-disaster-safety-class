using UnityEngine;

public class Fire_Step4_S8_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step4/Fire_Step4_S8_dialogues",
            "",
            "");
    }
}
