using UnityEngine;
using UnityEngine.UI;

public class LockerGameStarter : MonoBehaviour
{
    public GameObject startPanel;   // 시작 화면 패널
    public GameObject gamePanel;    // 미니게임 패널
    public Button startButton;      // 게임 시작 버튼

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        startPanel.SetActive(false); // 시작패널 비활성화
        gamePanel.SetActive(true);   // 게임패널 활성화
    }
}
