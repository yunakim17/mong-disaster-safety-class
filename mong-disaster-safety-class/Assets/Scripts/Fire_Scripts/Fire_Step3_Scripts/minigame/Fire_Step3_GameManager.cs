using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fire_Step3_GameManager : MonoBehaviour
{
    public Button nextButton;
    public Fire_Step3_PopupController popupController;

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        nextButton.onClick.AddListener(NextScene);
    }

    public void HandleSuccess()
    {
        nextButton.gameObject.SetActive(true);
        popupController.ShowPopup("찾았다! 이제 입과 코를 막고 복도로 나가자!");
    }

    public void NextScene()
    {
        popupController.popupPanel.SetActive(false);
        popupController.audioSource.Stop();
        SceneManager.LoadScene("Fire_Step3_S3");
    }
}