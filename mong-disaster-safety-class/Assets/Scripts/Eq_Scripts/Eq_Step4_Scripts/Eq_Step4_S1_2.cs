using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eq_Step4_S1_2 : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step4/Eq_Step4_S1-2_dialogues",
            "",
            "Eq_Step4_S2"
        );
    }
}
