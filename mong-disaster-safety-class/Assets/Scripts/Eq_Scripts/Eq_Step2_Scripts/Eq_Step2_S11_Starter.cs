using UnityEngine;

public class Eq_Step2_S11_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step2/Eq_Step2_S11_dialogues",
            "",
            "Eq_Step2_Badge" // ���� �� �̸�
        );
    }
}
