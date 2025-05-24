using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Step3_S6_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step3/Fire_Step3_S6_dialogues",
            "Dialogues/Fire_Step3/Fire_Step3_S6_choices",
            "Fire_Step3_S7");
    }
}
