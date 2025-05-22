using UnityEngine;

public class Fire_Step2_S8_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step2/Fire_Step2_S8_dialogues",
            "Dialogues/Fire_Step2/Fire_Step2_S8_choices",
            "Fire_Step2_S9");
    }
}
