using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DistanceBar : MonoBehaviour
{
    public Slider distanceSlider;
    public float totalDuration = 15f;
    public float timeScale = 1f;

    private float elapsedTime = 0f;
    private bool isEndingTriggered = false;

    public Color normalColor = new Color32(126, 200, 227, 255);
    public Color hitColor = new Color32(255, 69, 0, 255);
    private Image fillImage;

    [SerializeField] private GameObject schoolHallway;
    [SerializeField] private GameObject playground;

    [SerializeField] private Spawner obstacleSpawnerScript;
    [SerializeField] private BackgroundScroll backgroundScrollScript;
    [SerializeField] private Player playerScript;

    void Start()
    {
        // 슬라이더 색상 설정
        fillImage = distanceSlider.fillRect.GetComponent<Image>();
        fillImage.color = normalColor;
    }

    void Update()
    {
        if (!isEndingTriggered)
        {
            // 진행 시간에 따라 슬라이더 조정
            if (elapsedTime < totalDuration)
            {
                elapsedTime += Time.deltaTime * timeScale;
                float progress = elapsedTime / totalDuration;
                distanceSlider.value = progress;
            }
            else
                // 슬라이더가 다 차면 엔딩 진행
                TriggerEndingSequence();
        }  
    }

    // 슬라이더 색상 변경
    public void ChangeFillColor(Color newColor)
    {
        fillImage.color = newColor;
    }

    // 슬라이더가 다 차면 엔딩 진행
    private void TriggerEndingSequence()
    {
        isEndingTriggered = true;

        // 장애물 생성 중단
        if (obstacleSpawnerScript != null)
            obstacleSpawnerScript.StopSpawning();

        // 배경 전환
        if (schoolHallway != null)
            schoolHallway.SetActive(false);

        if (playground != null)
            playground.SetActive(true);

        // 배경 스크롤 중단
        if (backgroundScrollScript != null)
            backgroundScrollScript.StopScrolling();

        // 남아있는 장애물 제거
        Mover[] movers = FindObjectsOfType<Mover>();
        foreach (Mover mover in movers)
        {
            mover.RemoveSelf();
        }

        // 플레이어 점프 x
        if (playerScript != null)
            playerScript.StopPlayerControl();

        // 클리어 UI 활성화
        FindObjectOfType<GameManager>().ShowClearText();

        StartCoroutine(GoToNextSceneAfterDelay(2f));
    }

    private IEnumerator GoToNextSceneAfterDelay(float delay) //다음 씬 이동
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Eq_Step3_S4");  
    }
}
