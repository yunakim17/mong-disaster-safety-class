using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Step3_S6_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        if (fire_step3_BGM.Instance != null)
        {
            fire_step3_BGM.Instance.StopBGM();
            Debug.Log("씬6에서 BGM 정지됨");
        }
        dialogueManager.StartDialogue(
            "Dialogues/Fire_Step3/Fire_Step3_S6_dialogues",
            "Dialogues/Fire_Step3/Fire_Step3_S6_choices",
            "Fire_Step3_S7");
    }
}
