using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public static float GlobalVolume = 1f;

    public GameObject settingsPanel;
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {

        if (audioSource == null)
        {
            var bgm = FindObjectOfType<BGMPlayer>();
            if (bgm != null)
                audioSource = bgm.GetComponent<AudioSource>();

        if (audioSource != null)
        {
            audioSource.volume = 1f;

        }

        if (volumeSlider != null)
        {

            volumeSlider.onValueChanged.RemoveAllListeners();
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            volumeSlider.value = GlobalVolume;
        }

        if (audioSource != null)
        {
            audioSource.volume = GlobalVolume;
        }
    }

    public void OnVolumeChanged(float value)
    {
        GlobalVolume = value;

        if (audioSource != null)
        {
            audioSource.volume = value;

            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            volumeSlider.value = 1f;

        }
    }

    public void ToggleSettingsPanel()
    {

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(!settingsPanel.activeSelf);
        }

        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void OnVolumeChanged(float value)
    {
        if (audioSource == null)
        {
            return; 
        }

        audioSource.volume = value;
    }
}
