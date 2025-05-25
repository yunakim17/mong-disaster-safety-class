using UnityEngine;
using UnityEngine.UI;

public class Scene2Story : MonoBehaviour
{
    public GameObject mini1_Start;
    public GameObject mini1_Back;

    public Button startButton;       
    public GameObject leftButton;    
    public GameObject rightButton;   

    void Start()
    {
        mini1_Start.SetActive(true);
        mini1_Back.SetActive(false);

        startButton.gameObject.SetActive(true);
        leftButton.SetActive(false);
        rightButton.SetActive(false);

        if (startButton != null)
            startButton.onClick.AddListener(OnStartClicked);
    }

    public void OnStartClicked()
    {
        Debug.Log("시작 버튼 클릭됨 - mini1_Back으로 이동");

        mini1_Start.SetActive(false);
        mini1_Back.SetActive(true);

        startButton.gameObject.SetActive(false);
        leftButton.SetActive(true);
        rightButton.SetActive(true);
    }
}
