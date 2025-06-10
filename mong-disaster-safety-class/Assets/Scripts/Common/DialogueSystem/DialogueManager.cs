using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public AudioSource audioSource;
    public float typingSpeed = 0.05f;
    public GameObject nextButton;

    public Button btn1_2;
    public Button btn3_4;
    public Button btn5_6;
    public Button btn7;

    public Shake shakeTarget;

    private bool isTyping = false;
    private string currentText = "";
    private int currentLineIndex = 0;
    private List<DialogueLine> dialogueLines;
    private Dictionary<int, ChoiceEntry> choiceDict;
    private string nextSceneName = "";

    private HashSet<int> visitedIndices = new HashSet<int>();
    private List<int> requiredVisitedIndices = new List<int>();

    private Coroutine typingCoroutine; // ���� ���� ���� Ÿ���� �ڷ�ƾ ������

    public void StartDialogue(string dialogueFile, string choiceFile, string nextScene = "")
    {
        currentLineIndex = 0;
        nextSceneName = nextScene;

        choiceDict = new Dictionary<int, ChoiceEntry>();

        LoadDialogueFromJson(dialogueFile);
        LoadChoicesFromJson(choiceFile);

        string currentScene = SceneManager.GetActiveScene().name;

        ShowDialogue();

        if (currentScene == "Eq_Step1_S3")
        {
            btn1_2.onClick.AddListener(() => ShowSingleLine(1));
            btn3_4.onClick.AddListener(() => ShowSingleLine(2));
            btn5_6.onClick.AddListener(() => ShowSingleLine(3));
            btn7.onClick.AddListener(() => StartCoroutine(ShowTwoLinesSequentially(4, 5)));
            requiredVisitedIndices = new List<int> { 1, 2, 3, 4 };
            currentLineIndex = 4;
        }

        if (currentScene == "Fire_Step1_S3")
        {
            btn1_2.onClick.AddListener(() => ShowSingleLine(1));
            btn3_4.onClick.AddListener(() => ShowSingleLine(2));
            btn5_6.onClick.AddListener(() => ShowSingleLine(3));
            requiredVisitedIndices = new List<int> { 1, 2, 3 };
            currentLineIndex = 3;
        }
    }

    private void LoadDialogueFromJson(string fileName)
    {
        TextAsset json = Resources.Load<TextAsset>(fileName);
        if (json == null)
        {
            Debug.LogError($"JSON ���� �� ã��: {fileName}");
            return;
        }

        dialogueLines = new List<DialogueLine>(JsonHelper.FromJson<DialogueLine>(json.text));
    }

    private void LoadChoicesFromJson(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return;

        TextAsset json = Resources.Load<TextAsset>(fileName);

        if (json == null)
        {
            Debug.LogWarning($" ������ JSON ������ ã�� �� �����ϴ�: {fileName}");
            return;
        }

        ChoiceEntry[] choices = JsonHelper.FromJson<ChoiceEntry>(json.text);
        choiceDict = new Dictionary<int, ChoiceEntry>();
        foreach (var c in choices)
        {
            choiceDict[c.sequence] = c;
        }
    }

    public void ShowDialogue()
    {
        if (currentLineIndex >= dialogueLines.Count)
        {
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            return;
        }

        DialogueLine line = dialogueLines[currentLineIndex];
        int currentSequence = line.sequence;

        if (choiceDict != null && choiceDict.ContainsKey(currentSequence))
        {
            ChoiceManager.Instance.ShowChoice(choiceDict[currentSequence], OnChoiceResult);
            return;
        }

        currentText = line.dialogue_text;

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeSentence(currentText));

        if (line.sequence == 4 && shakeTarget != null)
        {
            shakeTarget.StopShake(1f);
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    public void OnTouch()
    {
        if (ChoiceManager.Instance != null && ChoiceManager.Instance.choicePanel.activeSelf) return;

        if (isTyping)
        {
            dialogueText.text = currentText;
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            isTyping = false;
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
            currentLineIndex++;
            ShowDialogue();
        }
    }

    private void OnChoiceResult(bool isCorrect)
    {
        if (isCorrect)
        {
            currentLineIndex++;

            if (currentLineIndex >= dialogueLines.Count)
            {
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene(nextSceneName);
                }
                return;
            }

            ShowDialogue();
        }
    }

    public void ShowSingleLine(int index)
    {
        Debug.Log($"[ShowSingleLine] index: {index}");
        if (index < 0 || index >= dialogueLines.Count)
        {
            Debug.LogWarning("�߸��� �ε��� ��û");
            return;
        }
        Debug.Log($"���: {dialogueLines[index].dialogue_text}");

        DialogueLine line = dialogueLines[index];
        currentText = line.dialogue_text;

        if (audioSource.isPlaying)
            audioSource.Stop();

        AudioClip clip = line.GetVoice();
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeSentence(currentText));

        visitedIndices.Add(index);

        string currentScene = SceneManager.GetActiveScene().name;
    }

    public IEnumerator ShowSingleLineAndWait(int index)
    {
        ShowSingleLine(index);
        yield return new WaitUntil(() => !isTyping && !audioSource.isPlaying);
    }

    private IEnumerator ShowTwoLinesSequentially(int firstIndex, int secondIndex)
    {
        yield return StartCoroutine(ShowSingleLineAndWait(firstIndex));
        yield return new WaitForSeconds(0.3f);
        yield return StartCoroutine(ShowSingleLineAndWait(secondIndex));
    }

    public void GoToNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void SetCurrentLineIndex(int index)
    {
        currentLineIndex = index;
    }

    public int GetCurrentLineIndex()
    {
        return currentLineIndex;
    }
}