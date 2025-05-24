using UnityEngine;

public class Fire_Step4_S1_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step4/Fire_Step4_S1_dialogues",
            "",
            "Fire_Step4_S2");
    }
}
