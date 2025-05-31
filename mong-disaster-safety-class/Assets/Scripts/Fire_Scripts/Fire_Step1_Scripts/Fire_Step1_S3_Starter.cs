using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Step1_S3_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step1/Fire_Step1_S3_dialogues",
            "",
            "Fire_Step1_S4");
    }
}
