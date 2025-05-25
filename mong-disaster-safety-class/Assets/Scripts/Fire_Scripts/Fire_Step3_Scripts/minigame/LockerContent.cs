using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LockerContent : MonoBehaviour, IPointerClickHandler
{
    public enum ContentType { Empty, Useless, Towel }
    public ContentType contentType;

    private Fire_Step3_GameManager gameManager;
    private Fire_Step3_PopupController popupController;

    void Start()
    {
        gameManager = FindObjectOfType<Fire_Step3_GameManager>();
        popupController = FindObjectOfType<Fire_Step3_PopupController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (popupController.IsPopupActive) return;

        switch (contentType)
        {
            case ContentType.Towel:
                gameManager.HandleSuccess();
                break;

            case ContentType.Useless:
                popupController.ShowPopup("연기로부터 입과 코를 막을만한 천이 필요해!");
                break;

            case ContentType.Empty:
                // 빈 내용물, 아무 처리 없음.
                break;
        }
    }
}