using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance; // �̱��� �ν��Ͻ� (�ٸ� ��ũ��Ʈ���� ���� ���� ����)

    [Header("UI Components")]
    public GameObject choicePanel; // ������ ��ü�� ���δ� �г�
    public TextMeshProUGUI questionText; // ���� �ؽ�Ʈ
    public Button option1Button; // ������ ��ư 1
    public Button option2Button; // ������ ��ư 2
    public TextMeshProUGUI feedbackText; // ���� �ǵ�� �ؽ�Ʈ
    public AudioSource audioSource; // ������ ���� ����� ����� �ҽ�

    private System.Action<bool> resultCallback; // ����/���� ����� ������ �ݹ�
    private ChoiceEntry currentChoice; // ���� �������� ������ ����

    private void Awake()
    {
        // �̱��� ���� �ʱ�ȭ
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        choicePanel.SetActive(false); // ���� �� ������ �г� ��Ȱ��ȭ
        feedbackText.gameObject.SetActive(false); // �ǵ�鵵 ��Ȱ��ȭ
    }

    // ������ �����ֱ�
    public void ShowChoice(ChoiceEntry choice, System.Action<bool> callback)
    {
        Debug.Log($"������ UI ǥ�� ��: {choice.question}");
        currentChoice = choice;
        resultCallback = callback;

        choicePanel.SetActive(true);
        feedbackText.gameObject.SetActive(false);

        questionText.text = choice.question; // ���� �ؽ�Ʈ ����
        option1Button.GetComponentInChildren<TextMeshProUGUI>().text = choice.option1;
        option2Button.GetComponentInChildren<TextMeshProUGUI>().text = choice.option2;

        // ���� �̺�Ʈ �����ϰ� ���� �߰�
        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();

        option1Button.onClick.AddListener(() => OnSelectOption(1));
        option2Button.onClick.AddListener(() => OnSelectOption(2));

        // ������ ���� ��� (voice_path ���)
        if (!string.IsNullOrEmpty(choice.voice_path))
        {
            AudioClip clip = Resources.Load<AudioClip>("Audio/" + choice.voice_path);
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }

    // ������ ��ư Ŭ�� �� ����
    private void OnSelectOption(int selectedOption)
    {
        if (selectedOption == currentChoice.correct_option)
        {
            // �����̸� �г� �ݰ� �ݹ� ȣ�� (true)
            choicePanel.SetActive(false);
            resultCallback?.Invoke(true);
        }
        else
        {
            // �����̸� �ǵ�� ���
            StartCoroutine(ShowFeedback());
            resultCallback?.Invoke(false);
        }
    }

    // ���� �ǵ�� �����ֱ� �ڷ�ƾ
    private System.Collections.IEnumerator ShowFeedback()
    {
        feedbackText.text = currentChoice.feedback;
        feedbackText.gameObject.SetActive(true);

        if (!string.IsNullOrEmpty(currentChoice.feedback_voice_path))
        {
            AudioClip feedbackClip = Resources.Load<AudioClip>("Audio/" + currentChoice.feedback_voice_path);
            if (feedbackClip != null)
            {
                audioSource.clip = feedbackClip;
                audioSource.Play();
            }
        }

        yield return new WaitForSeconds(2.5f);
    }
}


