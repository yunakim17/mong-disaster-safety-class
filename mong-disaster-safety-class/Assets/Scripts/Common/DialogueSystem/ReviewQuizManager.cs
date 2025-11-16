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
    public AudioClip questionVoiceClip; 
    public AudioClip feedbackVoiceClip; 

    //[Header("캐릭터 이미지")]
    //public Image characterImage;
    //public Sprite defaultSprite;
    //public Sprite correctAnswerSprite;

    [Header("정답 효과음")]
    public AudioSource audioSource;
    public AudioClip[] correctSounds;
    public AudioClip[] incorrectSounds;

    public AudioSource correctBGM;
    public AudioSource wrongBGM;


    public int stageId;
    public int totalQuestions = 0;

    public Animator mong_animator; //몽대장 말하는 애니메이션-애니메이터
    public Sprite mong_default_img; //말하기 끝나면 설정되는 입다문 이미지

    void Start()
    {
        questionText.text = quizQuestion;
        feedbackText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        oImageButton.SetActive(true);
        xImageButton.SetActive(true);


        mong_animator = GameObject.FindGameObjectWithTag("Mong_quiz").GetComponent<Animator>();
        mong_default_img = GameObject.FindGameObjectWithTag("Mong_quiz").GetComponent<Image>().sprite;

        correctBGM = GameObject.FindGameObjectWithTag("CorrectBGM").GetComponent<AudioSource>();
        wrongBGM = GameObject.FindGameObjectWithTag("WrongBGM").GetComponent<AudioSource>();


        //if (characterImage != null && defaultSprite != null)
        //{
        //    characterImage.sprite = defaultSprite;
        //}

        oImageButton.transform.localScale = Vector3.one;
        xImageButton.transform.localScale = Vector3.one;

        SetOXButtonsInteractable(false);

        StartCoroutine(PlayInitialQuestionAudio());
    }

    private void SetOXButtonsInteractable(bool interactable)
    {
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

    IEnumerator PlayInitialQuestionAudio()
    {
        if (audioSource != null && questionVoiceClip != null)
        {
            audioSource.clip = questionVoiceClip;
            audioSource.Play();


            ////
            if (mong_animator != null && mong_default_img != null)
            {

                mong_animator.enabled = true; //몽대장 애니메이션 재생
                StartCoroutine(WaitForAudioToEnd()); // 애니메이션 끄기 예약
            }


            yield return new WaitWhile(() => audioSource.isPlaying);
        }

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

            //if (characterImage != null && correctAnswerSprite != null)
            //{
            //    characterImage.sprite = correctAnswerSprite;
            //}

            feedbackText.gameObject.SetActive(true);

            correctBGM.Play();

            if (correctSounds != null && correctSounds.Length > 0)
            {
                selectedRandomClip = correctSounds[Random.Range(0, correctSounds.Length)];
            }

            if (mong_animator != null && mong_default_img != null)
            {

                mong_animator.enabled = true; //몽대장 애니메이션 재생
                mong_animator.SetTrigger("StartWink");
                //StartCoroutine(WaitForAudioToEnd()); // 애니메이션 끄기 예약
            }


        }
        else
        {
            feedbackText.color = new Color32(255, 80, 80, 255);
            feedbackText.gameObject.SetActive(true);

            wrongBGM.Play();

            if (incorrectSounds != null && incorrectSounds.Length > 0)
            {
                selectedRandomClip = incorrectSounds[Random.Range(0, incorrectSounds.Length)];
            }



            if (mong_animator != null && mong_default_img != null)
            {

                mong_animator.enabled = true; //몽대장 애니메이션 재생

                //StartCoroutine(WaitForAudioToEnd()); // 애니메이션 끄기 예약
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

            if (mong_animator != null && mong_default_img != null)
            {

                //mong_animator.enabled = true; //몽대장 애니메이션 재생
                // mong_animator.SetTrigger("StartWink");
                StartCoroutine(WaitForAudioToEnd()); // 애니메이션 끄기 예약
            }


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


    private IEnumerator WaitForAudioToEnd()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        if (mong_animator != null && mong_default_img != null)
        {
            mong_animator.enabled = false;
            mong_animator.GetComponent<Image>().sprite = mong_default_img; // 입 다문 이미지로 변경
        }
    }

}