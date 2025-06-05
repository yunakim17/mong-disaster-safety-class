using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject startPanel;   // StartPanel 오브젝트
    public GameObject gamePanel; // gamePanel 오브젝트
    public GameObject map_2F;

    public void OnStartButtonClick()
    {
        startPanel.SetActive(false);   // 대기 화면 숨기기
        gamePanel.SetActive(true);     // 게임 화면 보이기
        map_2F.SetActive(true);

        // 효과음 재생, 튜토리얼 시작, 타이머 시작 등 추가 가능
    }
}