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

    private void Start()
    {
        if (bgmSource != null)
        {
            bgmSource.volume = SettingsManager.GlobalVolume;
        }
    }

    private void Update()
    {
        if (bgmSource != null && bgmSource.volume != SettingsManager.GlobalVolume)
        {
            bgmSource.volume = SettingsManager.GlobalVolume;
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
            bgmSource.Stop(); // 기존 재생 중이면 정지
            bgmSource.clip = clip;
            bgmSource.volume = SettingsManager.GlobalVolume; 
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
        if (bgmSource != null && bgmSource.isPlaying)
        {
            bgmSource.Stop();
            Debug.Log("🛑 BGM 정지됨");
        }
    }
}
