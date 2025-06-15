using UnityEngine;

public class Fire_Step2_S7_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step2/Fire_Step2_S7_dialogues",
            "",
            "Fire_Step2_S12");
    }
}
