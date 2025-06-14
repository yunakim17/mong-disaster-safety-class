using UnityEngine;

public class fire_step3_BGM : MonoBehaviour
{
    public static fire_step3_BGM Instance;
    public AudioSource bgmSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("❌ 재생할 AudioClip이 null입니다.");
            return;
        }

        if (!bgmSource.isPlaying || bgmSource.clip != clip)
        {
            bgmSource.Stop(); // 혹시 기존에 재생 중이면 정지
            bgmSource.clip = clip;
            bgmSource.volume = 0.2f;
            bgmSource.loop = true;
            bgmSource.Play();

            Debug.Log($"✅ BGM 재생 시작: {clip.name} / 볼륨: {bgmSource.volume}");
        }
        else
        {
            Debug.Log("ℹ️ 이미 재생 중입니다.");
        }
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
            Debug.Log("🛑 BGM 정지됨");
        }
    }
}


