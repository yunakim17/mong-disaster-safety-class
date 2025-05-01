using UnityEngine;

public class Eq_Step2_S9_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S9_dialogues",
            "",
            "Eq_Step2_S10" // ¥Ÿ¿Ω æ¿ ¿Ã∏ß
        );
    }
}
