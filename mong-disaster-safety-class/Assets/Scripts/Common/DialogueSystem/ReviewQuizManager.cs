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
    [TextArea] public string correctFeedbackMessage; 
    [TextArea] public string wrongFeedbackMessage;  
    public bool correctIsO; 

    [Header("캐릭터 이미지")]
    public Image characterImage;         
    public Sprite defaultSprite;          
    public Sprite correctAnswerSprite;    

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
        bool isCorrect = (userChoseO == correctIsO);

        oImageButton.GetComponent<Button>().interactable = false;
        xImageButton.GetComponent<Button>().interactable = false;

        if (isCorrect)
        {
            feedbackText.text = correctFeedbackMessage;
            feedbackText.color = Color.green;

            if (characterImage != null && correctAnswerSprite != null)
            {
                characterImage.sprite = correctAnswerSprite;
            }
        }
        else
        {
            feedbackText.text = wrongFeedbackMessage;
            feedbackText.color = Color.red;

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

    IEnumerator PressEffect(GameObject buttonObj)
    {
        Vector3 originalScale = buttonObj.transform.localScale;
        buttonObj.transform.localScale = originalScale * 0.9f;
        yield return new WaitForSeconds(0.1f);
        buttonObj.transform.localScale = originalScale;
    }
}
