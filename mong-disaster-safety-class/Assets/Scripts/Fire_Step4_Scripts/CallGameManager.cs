using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallGameManager : MonoBehaviour
{
    public Text numberDisplay; // UI 텍스트 오브젝트
    private string currentInput = "";

    public GameObject callBtn;

    public GameObject[] numBtns;

    public bool callGameCleared;



    public static CallGameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        callGameCleared = false;
    }

    public void OnNumberButtonClick(string number)
    {
        if (currentInput.Length < 3 && MatchGameManager.Instance.isStarted)
        {
            currentInput += number;
            numberDisplay.text = currentInput;
        }
    }

    // 필요 시 입력 초기화용 메서드
    public void ClearInput()
    {
        currentInput = "";
        numberDisplay.text = "";
    }

    public void callBtnPressed()
    {
        
        
        //119가 맞는지 검사
        if( currentInput == "119") //119를 누르고 전화버튼 눌렀을 때(클리어 시)
        {
            
            print("119 전화연결 성공");
            callGameCleared = true;

            //선택지 매치 게임으로(선택지 패널 & 매칭 프레임들 보이게)
            MatchGameManager.Instance.showOptions();
            MatchGameManager.Instance.showFrames();

            //번호 버튼, 넘버디스플레이(텍스트), call버튼 사라지게
            for(int i = 0; i < numBtns.Length; i++)
            {
                numBtns[i].SetActive(false);
            }
            numberDisplay.gameObject.SetActive(false);
            callBtn.SetActive(false);
            
        }
        else
        {
            print("번호가 틀렸어! 다시 시도해봐!");
            ClearInput();
        }
    }
}
