using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LockerDoor : MonoBehaviour
{
    public GameObject doorImage;            // 사물함 문 이미지
    public GameObject contentImage;         // 사물함 안 내용물 (없을 수 있음)

    private bool isOpen = false;
    private Fire_Step3_PopupController popupController;

    void Start()
    {
        popupController = FindObjectOfType<Fire_Step3_PopupController>();

        // 내용물이 있는 경우에만 비활성화해둠
        if (contentImage != null)
            contentImage.SetActive(false);
    }

    public void OnDoorClicked()
    {
        // 팝업이 떠 있는 동안은 클릭 차단
        if (popupController != null && popupController.IsPopupActive)
            return;

        if (isOpen) return;

        if (doorImage != null)
            doorImage.SetActive(false);

        if (contentImage != null)
            contentImage.SetActive(true);

        StartCoroutine(CloseAfterDelay());
        isOpen = true;
    }

    IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        if (doorImage != null)
            doorImage.SetActive(true);

        if (contentImage != null)
            contentImage.SetActive(false);

        isOpen = false;
    }
}