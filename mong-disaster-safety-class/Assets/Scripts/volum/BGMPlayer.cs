using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMPlayer : MonoBehaviour
{
    private static BGMPlayer instance;

    private readonly string[] allowedScenes = {
        "Main", 
        "Eq_Main",
        "Fire_Main",
        "oxQuiz_Main",
        "MyBadge",
        "Ranking"
    };

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool isAllowed = false;

        foreach (var name in allowedScenes)
        {
            if (scene.name == name)
            {
                isAllowed = true;
                break;
            }
        }

        if (!isAllowed)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        var sm = FindObjectOfType<SettingsManager>();
        if (sm != null && sm.volumeSlider != null)
        {
            sm.volumeSlider.onValueChanged.RemoveListener(sm.OnVolumeChanged);
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
