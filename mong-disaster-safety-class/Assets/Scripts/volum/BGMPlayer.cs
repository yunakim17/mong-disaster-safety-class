using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class BGMPlayer : MonoBehaviour
{
    private readonly string[] allowedScenes = {
        "Main",
        "Eq_Main",
        "Fire_Main",
        "oxQuiz_Main",
        "MyBadge",
        "Ranking"
    };

    private static BGMPlayer instance;
    private AudioSource audioSource;
    private bool isInitialized = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
            {
                Destroy(gameObject);
                return;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
            isInitialized = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (audioSource != null)
        {
            float volume = 1.0f;
            audioSource.volume = volume;

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (audioSource == null) return;

        bool isAllowed = allowedScenes.Contains(scene.name);

        if (isAllowed)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    void OnDestroy()
    {
        if (isInitialized)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        var sm = FindObjectOfType<SettingsManager>();
        if (sm != null && sm.volumeSlider != null)
        {
            sm.volumeSlider.onValueChanged.RemoveListener(sm.OnVolumeChanged);
        }
    }
}