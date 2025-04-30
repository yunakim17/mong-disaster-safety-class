using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // 자막 텍스트 표시용
    public AudioSource audioSource; // 음성 재생용
    public float typingSpeed = 0.05f; // 타이핑 속도
    public GameObject nextButton; //다음 씬 이동 버튼

    public Button btn1_2;
    public Button btn3_4;
    public Button btn5_6;
    public Button btn7;

    public Shake shakeTarget;


    private bool isTyping = false; //현재 타이핑 중 여부
    private string currentText = ""; // 현재 출력중인 대사 텍스트
    private int currentLineIndex = 0; // 현재 몇 번째 대사인지
    private List<DialogueLine> dialogueLines; //대사리스트
    private Dictionary<int, ChoiceEntry> choiceDict; //선택지 데이터를 저장하는 딕셔너리(sequence 기준)
    private string nextSceneName = "";

    private HashSet<int> visitedIndices = new HashSet<int>(); //버튼별 대사 본 여부 추적용

    //대사와 선택지를 시작할 때 호출. 두개의 json파일 이름을 받음
    public void StartDialogue(string dialogueFile, string choiceFile, string nextScene = "")
    {
        currentLineIndex = 0;
        nextSceneName = nextScene;

        choiceDict = new Dictionary<int, ChoiceEntry>(); // 항상 초기화

        LoadDialogueFromJson(dialogueFile); // 대사 로드
        LoadChoicesFromJson(choiceFile); // 선택지 로드
        ShowDialogue(); // 첫 대사 출력

        if (SceneManager.GetActiveScene().name == "Eq_Step1_S3")
        {
            btn1_2.onClick.AddListener(() => ShowSingleLine(1));
            btn3_4.onClick.AddListener(() => ShowSingleLine(2));
            btn5_6.onClick.AddListener(() => ShowSingleLine(3));
            btn7.onClick.AddListener(() => ShowSingleLine(4));
        }

    }

    //대사 json 파일 로드
    private void LoadDialogueFromJson(string fileName)
    {
        TextAsset json = Resources.Load<TextAsset>(fileName);
        if (json == null)
        {
            Debug.LogError($"JSON 파일 못 찾음: {fileName}");
            return;
        }

        dialogueLines = new List<DialogueLine>(JsonHelper.FromJson<DialogueLine>(json.text));
    }

    //선택지 json파일 로드(sequence 기준으로 딕셔너리에 저장)
    private void LoadChoicesFromJson(string fileName)
    {
        if (string.IsNullOrEmpty(fileName)) return;  // 선택지 파일이 없는 경우

        TextAsset json = Resources.Load<TextAsset>(fileName);  // 전체 경로 넘겼으므로 그대로 받기

        if (json == null)
        {
            Debug.LogWarning($" 선택지 JSON 파일을 찾을 수 없습니다: {fileName}");
            return;
        }

        ChoiceEntry[] choices = JsonHelper.FromJson<ChoiceEntry>(json.text);
        Debug.Log($" 선택지 {choices.Length}개 로드됨");
        choiceDict = new Dictionary<int, ChoiceEntry>();
        foreach (var c in choices)
        {
            choiceDict[c.sequence] = c;
        }
    }

    // 현재 대사 출력(선택지가 있으면 ChoiceManager 호출)
    public void ShowDialogue()
    {
        Debug.Log($" 현재 대사 인덱스: {currentLineIndex}");
        if (currentLineIndex >= dialogueLines.Count)
        {
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                Debug.Log("대사 끝, 다음씬으로 이동: " + nextSceneName);
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("대사 끝! 이동할 씬 없음");
            }

            return;
        }
        DialogueLine line = dialogueLines[currentLineIndex];
        int currentSequence = line.sequence;

        Debug.Log($"현재 대사 인덱스: {currentLineIndex}, 시퀀스: {currentSequence}");
        // 선택지가 있는 시점이면 선택지 먼저 출력
        if (choiceDict != null && choiceDict.ContainsKey(currentSequence))
        {
            Debug.Log(" 선택지 표시 시점 도달!(시퀀스 기반)");
            ChoiceManager.Instance.ShowChoice(choiceDict[currentSequence], OnChoiceResult);
            return;
        }

        if (line == null) return;

        currentText = line.dialogue_text;
        dialogueText.text = "";
        // 음성재생
        AudioClip clip = line.GetVoice();
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        StartCoroutine(TypeSentence(currentText));// 타이핑시작

        if (line.sequence == 4 && shakeTarget != null)
        {
            Debug.Log("시퀀스 3 → UI 흔들림 점점 멈춤 시작");
            shakeTarget.StopShake(1f); // 1초 동안 점점 멈추기
        }

    }

    // 타이핑 효과 코루틴 (한 글자씩 출력)
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

    // 화면을 터치했을 때 호출됨
    public void OnTouch()
    {
        if (ChoiceManager.Instance != null && ChoiceManager.Instance.choicePanel.activeSelf)
        {
            Debug.Log("선택지 패널이 활성화된 상태에서는 터치로 대사 넘기기 불가");
            return;
        }
        if (isTyping)
        {
            dialogueText.text = currentText; // 전체 문장 표시
            StopAllCoroutines();
            isTyping = false;
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
            currentLineIndex++; // 다음 대사로 이동
            ShowDialogue();
        }
    }

    // 선택지 결과 콜백 함수 (정답/오답 처리)
    private void OnChoiceResult(bool isCorrect)
    {
        if (isCorrect)
        {
            currentLineIndex++; //다음 대사로 이동

            if (currentLineIndex >= dialogueLines.Count)
            {
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    Debug.Log("대사 끝, 다음 씬으로 이동: " + nextSceneName);
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log("대사 끝! 이동할 씬 없음");
                }
            }
            else
            {
                ShowDialogue();
            }
        }
        else
        {
            Debug.Log("오답! ChoiceManager가 피드백 처리");
        }
    }

    //S3 특정대사 인덱스
    public void ShowSingleLine(int index)
    {
        if (index < 0 || index >= dialogueLines.Count)
        {
            Debug.LogWarning("잘못된 대사 인덱스!");
            return;
        }

        DialogueLine line = dialogueLines[index];
        currentText = line.dialogue_text;
        dialogueText.text = "";

        if (audioSource.isPlaying)
            audioSource.Stop();

        AudioClip clip = line.GetVoice();
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentText));

        visitedIndices.Add(index);

        if (visitedIndices.Contains(1) && visitedIndices.Contains(2) && visitedIndices.Contains(3) && visitedIndices.Contains(4))
        {
            nextButton.SetActive(true);
        }

    }

    public void GoToNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

