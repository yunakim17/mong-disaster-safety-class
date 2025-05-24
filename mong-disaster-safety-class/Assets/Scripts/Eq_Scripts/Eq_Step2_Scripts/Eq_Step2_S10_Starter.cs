using UnityEngine;

public class Eq_Step2_S10_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S10_dialogues",
            "Dialogues/Eq_Step2/Eq_Step2_S10_choices",
            "Eq_Step2_S11" // ¥Ÿ¿Ω æ¿ ¿Ã∏ß
        );
    }
}
