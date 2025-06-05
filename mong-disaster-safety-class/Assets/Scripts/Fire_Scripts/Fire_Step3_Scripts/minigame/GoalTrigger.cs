using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalTrigger : MonoBehaviour
{
    public bool isStairGoal; // true면 계단, false면 현관
    public GameObject popupPanel;
    public AudioSource narrationAudio;

    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || hasTriggered) return;

        hasTriggered = true;

        if (isStairGoal)
        {
            popupPanel.SetActive(true);
            narrationAudio.Play();
            StartCoroutine(LoadNextSceneAfterAudio());
        }
        else
        {
            popupPanel.SetActive(true);
            narrationAudio.Play();
            StartCoroutine(LoadNextSceneAfterAudio2());
        }
    }

    private System.Collections.IEnumerator LoadNextSceneAfterAudio()
    {
        yield return new WaitWhile(() => narrationAudio.isPlaying);
        SceneManager.LoadScene("Fire_Step3_S5"); // 다음 씬 이름
    }
    private System.Collections.IEnumerator LoadNextSceneAfterAudio2()
    {
        yield return new WaitWhile(() => narrationAudio.isPlaying);
        SceneManager.LoadScene("Fire_Step3_S6"); // 다음 씬 이름
    }
}