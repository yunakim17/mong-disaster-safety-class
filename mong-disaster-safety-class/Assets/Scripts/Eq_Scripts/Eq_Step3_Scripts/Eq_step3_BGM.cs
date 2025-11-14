using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eq_step3_BGM : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bgmClip;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (bgmClip != null && audioSource != null)
        {
            audioSource.clip = bgmClip;
            audioSource.volume = SettingsManager.GlobalVolume;
            audioSource.loop = true;
            audioSource.Play();

            Debug.Log("지진 3단계 미니게임 BGM 재생 시작");
        }
        else
        {
            Debug.LogWarning("BGM 클립이나 AudioSource가 없습니다.");
        }
    }

    private void Update()
    {
        if (audioSource != null && audioSource.volume != SettingsManager.GlobalVolume)
        {
            audioSource.volume = SettingsManager.GlobalVolume;
        }
    }
}

