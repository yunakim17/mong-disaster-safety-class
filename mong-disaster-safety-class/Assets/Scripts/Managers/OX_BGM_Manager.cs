using UnityEngine;
using UnityEngine.SceneManagement;

public class OX_BGM_Manager : MonoBehaviour
{
    private static OX_BGM_Manager instance;
    private const string MAIN_SCENE_NAME = "Main";
    private const string OX_QUIZ_MAIN_SCENE = "oxQuiz_Main";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.volume = SettingsManager.GlobalVolume;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == OX_QUIZ_MAIN_SCENE || scene.name == MAIN_SCENE_NAME)
        {
            var audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Stop();
            }

            SceneManager.sceneLoaded -= OnSceneLoaded;
            instance = null;
            Destroy(gameObject);
            Debug.Log($"OX_BGM_Manager: {scene.name} ·Îµå. ÄûÁî BGM Á¤Áö ¹× ÆÄ±«.");
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}