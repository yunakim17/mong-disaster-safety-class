using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Step4_S2_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step4/Fire_Step4_S2_dialogues",
            "Dialogues/Fire_Step4/Fire_Step4_S2_choices",
            "Fire_Step4_S3");
    }
}
