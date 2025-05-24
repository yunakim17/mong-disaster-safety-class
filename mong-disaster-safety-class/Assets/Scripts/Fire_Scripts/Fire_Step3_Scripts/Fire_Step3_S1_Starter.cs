using UnityEngine;

public class Fire_Step3_S1_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step3/Fire_Step3_S1_dialogues",
            "",
            "Fire_Step3_S2");
    }
}
