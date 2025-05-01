using UnityEngine;

public class Eq_Step2_S6_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S6_dialogues",
            "Dialogues/Eq_Step2/Eq_Step2_S6_choices",
            "Eq_Step2_S7" // ¥Ÿ¿Ω æ¿ ¿Ã∏ß
        );
    }
}
