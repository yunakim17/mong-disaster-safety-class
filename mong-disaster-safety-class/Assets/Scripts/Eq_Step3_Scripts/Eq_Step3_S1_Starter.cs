using UnityEngine;

public class Eq_Step3_S1_Starter : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public Shake shakeTarget;  // 흔들 대상 오브젝트 연결

    void Start()
    {
        if (shakeTarget != null)
        {
            shakeTarget.StartShake(0.3f);
        }

        dialogueManager.StartDialogue(
            "Dialogues/Eq_Step3/Eq_Step3_S1_dialogues",
            "Dialogues/Eq_Step3/Eq_Step3_S1_choices",
            "Eq_Step3_S2"
        );
    }

    void StopShakeSmoothly()
    {
        if (shakeTarget != null)
        {
            shakeTarget.StopShake(1f); // 1초 동안 서서히 멈춤
        }
    }
}
