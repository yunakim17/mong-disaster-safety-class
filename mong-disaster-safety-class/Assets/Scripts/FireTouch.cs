using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float longPressDuration = 3f; // ����ġ�� �νĵǴ� �ð�
    public Vector3 enlargedScale = new Vector3(2.5f, 2.5f, 1f); // Ŀ�� ũ��

    private Vector3 originalScale;
    private float pressTime;
    private bool isPointerDown = false;
    private bool isScaling = false;
    private bool hasLongPressed = false;

    public SirenCtrl SirenCtrl;

    public DialogueManager dialogueManager;

    void Start()
    {
        originalScale = transform.localScale;
     
    }

    void Update()
    {
        if (isPointerDown)
        {
            pressTime += Time.deltaTime;
            float t = Mathf.Clamp01(pressTime / longPressDuration);
            transform.localScale = Vector3.Lerp(originalScale, enlargedScale, t);

            if (pressTime >= 2f && !hasLongPressed)
            {
                hasLongPressed = true; // �� ���� ����ǰ�
                SirenCtrl.StartPulse(); // ���⼭ �ٷ� ����˴ϴ�

                SirenCtrl.GetComponent<AudioSource>().Play();

                if (dialogueManager != null)
                {
                    dialogueManager.StartDialogue(
                        "Dialogues/Fire_Step1/Fire_Step1_S2_dialogues",
                        "",
                        "Fire_Step1_S3");
                }
            }
        }

        if (!isPointerDown && isScaling)
        {
            // ���� �� �� �ٽ� ���� ũ��� ������ �ǵ���
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 5f);
            if (Vector3.Distance(transform.localScale, originalScale) < 0.01f)
            {
                transform.localScale = originalScale;
                isScaling = false;
            }

            hasLongPressed = false; // �� ���� ����ǰ�
            SirenCtrl.GetComponent<AudioSource>().Stop();
            SirenCtrl.StopPulse();

        }

       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        pressTime = 0f;
        isScaling = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        isScaling = true;
        hasLongPressed = false;
    }
}
