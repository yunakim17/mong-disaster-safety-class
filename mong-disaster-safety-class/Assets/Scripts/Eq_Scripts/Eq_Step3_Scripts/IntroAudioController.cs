using UnityEngine;
using UnityEngine.UI;

public class IntroAudioController : MonoBehaviour
{
    public AudioSource introAudio;         // 자동 재생할 오디오
    public Button startButton;             // 버튼 참조

    void Start()
    {
        if (introAudio != null)
        {
            introAudio.Play(); // 씬 시작 시 자동 재생
        }

        if (startButton != null)
        {
            startButton.onClick.AddListener(StopIntroAudio); // 버튼 클릭 시 오디오 정지
        }
    }

    void StopIntroAudio()
    {
        if (introAudio != null && introAudio.isPlaying)
        {
            introAudio.Stop();
            Debug.Log("인트로 오디오 정지됨");
        }
    }
}
