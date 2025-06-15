using UnityEngine;
using System.Collections;

public class SpeakerEffectController : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject speakerSprite; // ������ų ��������Ʈ ������Ʈ

    private Coroutine pulseCoroutine = null;
    private int currentIdx = -1;

    // �⺻/Ȯ�� ������ ����
    private Vector3 originalScale = Vector3.one * 0.85f;
    private Vector3 enlargedScale = Vector3.one * 0.95f;

    void Start()
    {
        if (speakerSprite != null)
        {
            speakerSprite.transform.localScale = originalScale;
        }
    }

    void Update()
    {
        if (dialogueManager == null || speakerSprite == null) return;

        int idx = dialogueManager.GetCurrentLineIndex();

        if (idx != currentIdx)
        {
            currentIdx = idx;

            if (currentIdx ==1 || currentIdx == 2) // 3��° ����� ��
            {
                if (pulseCoroutine == null)
                    pulseCoroutine = StartCoroutine(PulseEffect());
            }
            else
            {
                if (pulseCoroutine != null)
                {
                    StopCoroutine(pulseCoroutine);
                    pulseCoroutine = null;

                    // ������ ������� ����
                    speakerSprite.transform.localScale = originalScale;
                }
            }
        }
    }

    IEnumerator PulseEffect()
    {
        while (true)
        {
            speakerSprite.transform.localScale = enlargedScale;
            yield return new WaitForSeconds(0.1f);
            speakerSprite.transform.localScale = originalScale;
            yield return new WaitForSeconds(0.1f);
        }
    }
}