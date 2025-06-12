using UnityEngine;
using UnityEngine.SceneManagement;

public class OX_BGM_Manager : MonoBehaviour
{
    private static OX_BGM_Manager instance;

    private void Start()
    {
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
            DontDestroyOnLoad(gameObject); // 씬 이동 시 파괴되지 않게 설정
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }
}
