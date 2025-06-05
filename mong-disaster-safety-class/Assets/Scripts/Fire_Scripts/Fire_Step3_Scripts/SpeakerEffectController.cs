using UnityEngine;
using System.Collections;

public class SpeakerEffectController : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject speakerSprite; // 진동시킬 스프라이트 오브젝트

    private Coroutine pulseCoroutine = null;
    private int currentIdx = -1;

    // 기본/확대 스케일 설정
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

            if (currentIdx == 2) // 3번째 대사일 때
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

                    // 스케일 원래대로 복원
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