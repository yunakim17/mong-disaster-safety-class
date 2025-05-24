using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eq_Step2_S3_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S3_dialogues",
            "",
            "Eq_Step2_S4" // ¥Ÿ¿Ω æ¿ ¿Ã∏ß
        );
    }
}
