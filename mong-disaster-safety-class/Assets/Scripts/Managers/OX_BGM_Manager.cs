using UnityEngine;
using UnityEngine.SceneManagement;

public class OX_BGM_Manager : MonoBehaviour
{
    private static OX_BGM_Manager instance;

    private void Start()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.volume = SettingsManager.GlobalVolume;

        if (SceneManager.GetActiveScene().name == "QuizResult")
        {
            GameObject bgm = GameObject.Find("BGMManager");
            Destroy(bgm);
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
