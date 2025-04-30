using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance; // 싱글톤 인스턴스 (다른 스크립트에서 쉽게 접근 가능)

    [Header("UI Components")]
    public GameObject choicePanel; // 선택지 전체를 감싸는 패널
    public TextMeshProUGUI questionText; // 질문 텍스트
    public Button option1Button; // 선택지 버튼 1
    public Button option2Button; // 선택지 버튼 2
    public TextMeshProUGUI feedbackText; // 오답 피드백 텍스트
    public AudioSource audioSource; // 선택지 음성 재생용 오디오 소스

    private System.Action<bool> resultCallback; // 정답/오답 결과를 전달할 콜백
    private ChoiceEntry currentChoice; // 현재 보여지는 선택지 정보

    private void Awake()
    {
        // 싱글톤 패턴 초기화
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        choicePanel.SetActive(false); // 시작 시 선택지 패널 비활성화
        feedbackText.gameObject.SetActive(false); // 피드백도 비활성화
    }

    // 선택지 보여주기
    public void ShowChoice(ChoiceEntry choice, System.Action<bool> callback)
    {
        Debug.Log($"선택지 UI 표시 중: {choice.question}");
        currentChoice = choice;
        resultCallback = callback;

        choicePanel.SetActive(true);
        feedbackText.gameObject.SetActive(false);

        questionText.text = choice.question; // 질문 텍스트 설정
        option1Button.GetComponentInChildren<TextMeshProUGUI>().text = choice.option1;
        option2Button.GetComponentInChildren<TextMeshProUGUI>().text = choice.option2;

        // 이전 이벤트 제거하고 새로 추가
        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();

        option1Button.onClick.AddListener(() => OnSelectOption(1));
        option2Button.onClick.AddListener(() => OnSelectOption(2));

        // 선택지 음성 재생 (voice_path 사용)
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

    // 선택지 버튼 클릭 시 실행
    private void OnSelectOption(int selectedOption)
    {
        if (selectedOption == currentChoice.correct_option)
        {
            // 정답이면 패널 닫고 콜백 호출 (true)
            choicePanel.SetActive(false);
            resultCallback?.Invoke(true);
        }
        else
        {
            // 오답이면 피드백 출력
            StartCoroutine(ShowFeedback());
            resultCallback?.Invoke(false);
        }
    }

    // 오답 피드백 보여주기 코루틴
    private System.Collections.IEnumerator ShowFeedback()
    {
        feedbackText.text = currentChoice.feedback;
        feedbackText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
    }
}


