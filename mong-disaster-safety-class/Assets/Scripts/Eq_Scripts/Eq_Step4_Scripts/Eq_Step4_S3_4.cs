using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eq_Step4_S3_4 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step4/Eq_Step4_S3-4_dialogues",
            "",
            "Eq_Step4_S4"
        );
    }
}
