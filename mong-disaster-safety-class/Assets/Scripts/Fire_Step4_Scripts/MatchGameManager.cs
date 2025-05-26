using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchGameManager : MonoBehaviour
{
    public static MatchGameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public bool isStarted;
    public bool matchGameCleared;

    public bool line1_Matched;
    public bool line2_Matched;
    public bool line3_Matched;

    public GameObject[] frames;
    public GameObject[] options;


    public GameObject clearPanel;
    public GameObject startPanel;

    public GameObject wrongOrderPanel;
    public GameObject matchedPanel;
    public GameObject wrongAnswerPanel;


    // Start is called before the first frame update
    void Start()
    {
        isStarted = false;
        matchGameCleared = false;

        line1_Matched = false;
        line2_Matched = false;
        line3_Matched = false;

        startPanel.SetActive(true);
        clearPanel.SetActive(false);

        wrongOrderPanel.SetActive(false);
        matchedPanel.SetActive(false);
        wrongAnswerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!matchGameCleared && line1_Matched && line2_Matched && line3_Matched)
        {
            Invoke("showClearPanel", 2f);
            print("게임 클리어!");
            matchGameCleared = true; //한번만 실행되도록 
        }
    }

    public void showClearPanel()
    {
        clearPanel.SetActive(true);
    }

    public void showWrongOrderPanel()
    {
        wrongOrderPanel.SetActive(true);
        StartCoroutine(HideAfterDelay(wrongOrderPanel, 2f));
    }

    public void showMatchedPanel()
    {
        matchedPanel.SetActive(true);
        StartCoroutine(HideAfterDelay(matchedPanel, 2f));
    }

 
    public void showWrongAnswerPanel()
    {
        wrongAnswerPanel.SetActive(true);
        StartCoroutine(HideAfterDelay(wrongAnswerPanel, 2f));
    }

    public void startBtnPressed()
    {
        //시작 패널 숨기기
        startPanel.SetActive(false);
        isStarted = true;
    }

    public void showOptions()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].SetActive(true);
        }
    }

    public void showFrames()
    {
        for(int i = 0;i < frames.Length;i++)
        {
            frames[i].SetActive(true);
        }
    }

    private IEnumerator HideAfterDelay(GameObject panel, float delay) // delay초 뒤에 panel 게임 오브젝트 비활성화하기
    {
        yield return new WaitForSeconds(delay);
        panel.SetActive(false);
    }
}
