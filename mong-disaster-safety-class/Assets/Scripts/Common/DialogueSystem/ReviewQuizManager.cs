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
    public GameObject oImageButton;  // O 이미지 (선택 + 버튼)
    public GameObject xImageButton;  // X 이미지 (선택 + 버튼)
    public Button nextButton;

    [Header("퀴즈 설정")]
    [TextArea] public string quizQuestion;
    [TextArea] public string correctFeedbackMessage; // 정답 시 피드백
    [TextArea] public string wrongFeedbackMessage;   // 오답 시 피드백
    public bool correctIsO; // true면 O가 정답, false면 X가 정답

    [Header("캐릭터 이미지")]
    public Image characterImage;          // 캐릭터 이미지 (mong_speak_0 오브젝트)
    public Sprite defaultSprite;          // 기본 상태 이미지
    public Sprite correctAnswerSprite;    // 정답 맞췄을 때 이미지 (mong)

    void Start()
    {
        // UI 초기화
        questionText.text = quizQuestion;
        feedbackText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        oImageButton.SetActive(true);
        xImageButton.SetActive(true);

        // 캐릭터 초기 이미지 설정
        if (characterImage != null && defaultSprite != null)
        {
            characterImage.sprite = defaultSprite;
        }
    }

    public void SelectO()
    {
        StartCoroutine(PressEffect(oImageButton));
        CheckAnswer(userChoseO: true);
    }

    public void SelectX()
    {
        StartCoroutine(PressEffect(xImageButton));
        CheckAnswer(userChoseO: false);
    }

    private void CheckAnswer(bool userChoseO)
    {
        bool isCorrect = (userChoseO == correctIsO);

        // 버튼 비활성화
        oImageButton.GetComponent<Button>().interactable = false;
        xImageButton.GetComponent<Button>().interactable = false;

        // 피드백 표시
        if (isCorrect)
        {
            feedbackText.text = correctFeedbackMessage;
            feedbackText.color = Color.green;

            // 캐릭터 이미지 변경
            if (characterImage != null && correctAnswerSprite != null)
            {
                characterImage.sprite = correctAnswerSprite;
            }
        }
        else
        {
            feedbackText.text = wrongFeedbackMessage;
            feedbackText.color = Color.red;

            // 오답이면 기본 이미지 유지 or 되돌리기
            if (characterImage != null && defaultSprite != null)
            {
                characterImage.sprite = defaultSprite;
            }
        }

        feedbackText.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // 버튼 눌림 효과
    IEnumerator PressEffect(GameObject buttonObj)
    {
        Vector3 originalScale = buttonObj.transform.localScale;
        buttonObj.transform.localScale = originalScale * 0.9f;
        yield return new WaitForSeconds(0.1f);
        buttonObj.transform.localScale = originalScale;
    }
}
