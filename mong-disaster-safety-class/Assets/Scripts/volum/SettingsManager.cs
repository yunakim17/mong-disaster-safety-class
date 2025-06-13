using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.volume = 1f;
        }

        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            volumeSlider.value = 1f;
        }
    }

    public void ToggleSettingsPanel()
    {
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
