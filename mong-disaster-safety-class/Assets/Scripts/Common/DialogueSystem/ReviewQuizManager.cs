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
    public GameObject oImageButton;
    public GameObject xImageButton;
    public Button nextButton;

    [Header("퀴즈 설정")]
    [TextArea] public string quizQuestion;
    [TextArea] public string feedbackMessage;
    public bool correctIsO;

    [Header("캐릭터 이미지")]
    public Image characterImage;
    public Sprite defaultSprite;
    public Sprite correctAnswerSprite;

    [Header("정답 효과음")]
    public AudioSource audioSource;
    public AudioClip correctSound;

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
        oImageButton.GetComponent<Button>().interactable = false;
        xImageButton.GetComponent<Button>().interactable = false;

        if (userChoseO == correctIsO)
        {
            feedbackText.color = Color.white;
            feedbackText.text = feedbackMessage;

            if (characterImage != null && correctAnswerSprite != null)
            {
                characterImage.sprite = correctAnswerSprite;
            }

            feedbackText.gameObject.SetActive(true);

            if (audioSource != null && correctSound != null)
            {
                audioSource.clip = correctSound;
                audioSource.Play();
                StartCoroutine(ShowNextButtonAfterAudio());
            }
            else
            {
                nextButton.gameObject.SetActive(true);
            }
        }
        else
        {
            feedbackText.color = new Color32(255, 80, 80, 255);
            feedbackText.text = "아니야! " + feedbackMessage;

            feedbackText.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
        }
    }

    IEnumerator ShowNextButtonAfterAudio()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        nextButton.gameObject.SetActive(true);
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
        buttonObj.transform.localScale = originalScale;
    }
}
