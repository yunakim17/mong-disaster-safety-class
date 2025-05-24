using UnityEngine;
using UnityEngine.UI;

public class Scene2Story : MonoBehaviour
{
    public GameObject kitchen;
    public GameObject mini1_Start;
    public GameObject mini1_Back;

    public Button nextButton;
    public Button startButton;       // Eq4_miniGame_2
    public GameObject leftButton;    // Eq4_miniGame_31
    public GameObject rightButton;   // Eq4_miniGame_30

    void Start()
    {
        kitchen.SetActive(true);
        mini1_Start.SetActive(false);
        mini1_Back.SetActive(false);

        // 초기 버튼 상태
        startButton.gameObject.SetActive(false);
        leftButton.SetActive(false);
        rightButton.SetActive(false);

        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextClicked);

        if (startButton != null)
            startButton.onClick.AddListener(OnStartClicked);
    }

    public void OnNextClicked()
    {
        Debug.Log("NextButton 클릭됨 - mini1_Start로 이동");

        kitchen.SetActive(false);
        mini1_Start.SetActive(true);
        mini1_Back.SetActive(false);

        nextButton.gameObject.SetActive(false);

        // 시작 버튼만 보이게
        startButton.gameObject.SetActive(true);
        leftButton.SetActive(false);
        rightButton.SetActive(false);
    }

    public void OnStartClicked()
    {
        Debug.Log("시작 버튼 클릭됨 - mini1_Back으로 이동");

        mini1_Start.SetActive(false);
        mini1_Back.SetActive(true);

        // 선택 버튼만 보이게
        startButton.gameObject.SetActive(false);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
    }
}
