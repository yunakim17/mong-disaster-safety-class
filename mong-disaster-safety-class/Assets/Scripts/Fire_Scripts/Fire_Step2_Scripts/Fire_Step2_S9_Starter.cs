using UnityEngine;

public class Fire_Step2_S9_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step2/Fire_Step2_S9_dialogues",
            "",
            "Fire_Step2_S10");
    }
}
