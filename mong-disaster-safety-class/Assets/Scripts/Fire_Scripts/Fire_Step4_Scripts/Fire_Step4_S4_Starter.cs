using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Step4_S4_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step4/Fire_Step4_S4_dialogues",
            "Dialogues/Fire_Step4/Fire_Step4_S4_choices",
            "Fire_Step4_S5");
    }
}
