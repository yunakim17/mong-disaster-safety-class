using UnityEngine;

public class Fire_Step2_S5_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step2/Fire_Step2_S5_dialogues",
            "",
            "Fire_Step2_S6");
    }
}
