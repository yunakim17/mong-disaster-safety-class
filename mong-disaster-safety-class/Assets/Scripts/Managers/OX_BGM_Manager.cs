using UnityEngine;
using UnityEngine.SceneManagement;

public class OX_BGM_Manager : MonoBehaviour
{
    private static OX_BGM_Manager instance;
    private const string MAIN_SCENE_NAME = "Main"; 

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
        audioSource.volume = SettingsManager.GlobalVolume;

        if (SceneManager.GetActiveScene().name != MAIN_SCENE_NAME)
        {
            GameObject mainBGM = GameObject.Find("BGMPlayer");
            if (mainBGM != null)
            {
                Destroy(mainBGM);
            }
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == MAIN_SCENE_NAME)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            instance = null;

            Destroy(gameObject);
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