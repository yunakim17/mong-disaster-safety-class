using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ReviewQuizManager : MonoBehaviour
{
    [Header("UI 요소")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI feedbackText;
    public GameObject oImageButton; // O 이미지 버튼 오브젝트 (Button 컴포넌트 포함)
    public GameObject xImageButton; // X 이미지 버튼 오브젝트 (Button 컴포넌트 포함)
    public Button nextButton;

    [Header("퀴즈 설정")]
    [TextArea] public string quizQuestion;
    [TextArea] public string feedbackMessage;
    public bool correctIsO;
    public AudioClip questionVoiceClip; // 문제 보이스 클립
    public AudioClip feedbackVoiceClip; //  문제별 피드백 보이스 클립

    [Header("캐릭터 이미지")]
    public Image characterImage;
    public Sprite defaultSprite;
    public Sprite correctAnswerSprite;

    [Header("정답 효과음")]
    public AudioSource audioSource;
    public AudioClip[] correctSounds;
    public AudioClip[] incorrectSounds;

    public int stageId;
    public int totalQuestions = 0;

    void Start()
    {
        questionText.text = quizQuestion;
        feedbackText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        oImageButton.SetActive(true);
        xImageButton.SetActive(true);

        if (characterImage != null && defaultSprite != null)
        {
            characterImage.sprite = defaultSprite;
        }

        oImageButton.transform.localScale = Vector3.one;
        xImageButton.transform.localScale = Vector3.one;

        // 1. O/X 버튼을 비활성화하여 입력 대기 상태로 시작
        SetOXButtonsInteractable(false);

        // 2. 문제 보이스 재생 코루틴 시작
        StartCoroutine(PlayInitialQuestionAudio());
    }

    // O/X 버튼의 상호작용 가능/불가능을 설정하는 헬퍼 함수
    private void SetOXButtonsInteractable(bool interactable)
    {
        //  중요: OImageButton과 XImageButton에 오브젝트가 할당되었는지 확인해야 합니다.
        // 그리고 그 오브젝트에 Button 컴포넌트가 붙어 있어야 합니다.
        if (oImageButton != null)
        {
            Button buttonO = oImageButton.GetComponent<Button>();
            if (buttonO != null)
            {
                buttonO.interactable = interactable;
            }
        }

        if (xImageButton != null)
        {
            Button buttonX = xImageButton.GetComponent<Button>();
            if (buttonX != null)
            {
                buttonX.interactable = interactable;
            }
        }
    }

    // 문제 보이스를 재생하고 사용자 입력을 기다리는 코루틴
    IEnumerator PlayInitialQuestionAudio()
    {
        // 문제 보이스 재생
        if (audioSource != null && questionVoiceClip != null)
        {
            audioSource.clip = questionVoiceClip;
            audioSource.Play();

            // 보이스 재생이 끝날 때까지 대기
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        // 재생이 끝나면 O/X 버튼 활성화 (사용자 입력 가능)
        SetOXButtonsInteractable(true);
    }

    public void SelectO()
    {
        StartCoroutine(PressEffect(oImageButton));
        HighlightSelection(true);
        CheckAnswer(userChoseO: true);
    }

    public void SelectX()
    {
        StartCoroutine(PressEffect(xImageButton));
        HighlightSelection(false);
        CheckAnswer(userChoseO: false);
    }

    private void CheckAnswer(bool userChoseO)
    {
        SetOXButtonsInteractable(false);

        AudioClip selectedRandomClip = null;

        if (userChoseO == correctIsO)
        {
            QuizSum.AddCorrect();
            feedbackText.color = Color.white;
            feedbackText.text = feedbackMessage;

            if (characterImage != null && correctAnswerSprite != null)
            {
                characterImage.sprite = correctAnswerSprite;
            }

            feedbackText.gameObject.SetActive(true);

            if (correctSounds != null && correctSounds.Length > 0)
            {
                selectedRandomClip = correctSounds[Random.Range(0, correctSounds.Length)];
            }
        }
        else
        {
            // 오답 처리
            feedbackText.color = new Color32(255, 80, 80, 255);
            feedbackText.gameObject.SetActive(true);

            if (incorrectSounds != null && incorrectSounds.Length > 0)
            {
                selectedRandomClip = incorrectSounds[Random.Range(0, incorrectSounds.Length)];
            }
        }

        if (audioSource != null && (selectedRandomClip != null || feedbackVoiceClip != null))
        {
            StartCoroutine(PlayAudioSequenceAndShowNextButton(selectedRandomClip));
        }
        else
        {

            nextButton.gameObject.SetActive(true);
        }
    }

    IEnumerator PlayAudioSequenceAndShowNextButton(AudioClip randomClip)
    {
        if (randomClip != null && audioSource != null)
        {
            audioSource.clip = randomClip;
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        if (feedbackVoiceClip != null && audioSource != null)
        {
            audioSource.clip = feedbackVoiceClip;
            audioSource.Play();
            yield return new WaitWhile(() => audioSource.isPlaying);
        }

        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(true);
        }
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator PressEffect(GameObject buttonObj)
    {
        Vector3 originalScale = buttonObj.transform.localScale;
        buttonObj.transform.localScale = originalScale * 0.9f;
        yield return new WaitForSeconds(0.1f);
    }

    private void HighlightSelection(bool selectedO)
    {
        float selectedScale = 1.1f;
        float unselectedScale = 0.8f;

        if (selectedO)
        {
            oImageButton.transform.localScale = Vector3.one * selectedScale;
            xImageButton.transform.localScale = Vector3.one * unselectedScale;
        }
        else
        {
            oImageButton.transform.localScale = Vector3.one * unselectedScale;
            xImageButton.transform.localScale = Vector3.one * selectedScale;
        }
    }

    public void GoToResultScene()
    {
        PlayerPrefs.SetInt("stage_id_quiz", stageId);
        PlayerPrefs.SetInt("quiz_total", totalQuestions);
        SceneManager.LoadScene("QuizResult");
    }
}