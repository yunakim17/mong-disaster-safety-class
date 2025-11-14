using UnityEngine;

public class TouchSoundManager : MonoBehaviour
{
    public static TouchSoundManager Instance;

    public AudioClip clickSound;
    private AudioSource audioSource;

    void Awake()
    {
        // ΩÃ±€≈Ê ∆–≈œ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = 0.5f;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClickSound()
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
