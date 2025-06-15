using UnityEngine;

public class escapeGamestartManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject map_2F;

    public AudioSource dialogueAudioSource;

    public void OnStartButtonClick()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        map_2F.SetActive(true);

        // 시작 설명 오디오 끄기
        if (dialogueAudioSource != null && dialogueAudioSource.isPlaying)
        {
            dialogueAudioSource.Stop();
            Debug.Log("시작 설명 오디오 정지됨");
        }

        // BGM 재생
        AudioClip clip = Resources.Load<AudioClip>("BGMs/fire_step3_minigame");

        if (clip == null)
        {
            Debug.LogError(" AudioClip이 Resources에서 로드되지 않았습니다.");
            return;
        }

        if (fire_step3_BGM.Instance == null)
        {
            Debug.LogError(" fire_step3_BGM 인스턴스가 null입니다.");
            return;
        }

        fire_step3_BGM.Instance.PlayBGM(clip);
    }
}
