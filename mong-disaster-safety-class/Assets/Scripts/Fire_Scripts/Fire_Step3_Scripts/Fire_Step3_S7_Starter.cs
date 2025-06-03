using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Step3_S7_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step3/Fire_Step3_S7_dialogues",
            "",
            "Fire_Step3_Badge");
    }
}
