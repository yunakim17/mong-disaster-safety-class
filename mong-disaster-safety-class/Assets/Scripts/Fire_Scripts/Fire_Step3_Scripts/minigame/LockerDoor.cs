using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LockerDoor : MonoBehaviour
{
    public GameObject doorImage;   // 문 이미지 (버튼 자신)
    public GameObject contentImage; // 내용물 (내용물에 LockerContent 스크립트 있음)

    Button doorButton;

    void Start()
    {
        doorButton = GetComponent<Button>();
        doorButton.onClick.AddListener(OpenDoor);

        contentImage.SetActive(false);
    }

    void OpenDoor()
    {
        doorImage.SetActive(false);
        contentImage.SetActive(true);

        StartCoroutine(CloseDoorAfterDelay());
    }

    IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        doorImage.SetActive(true);
        contentImage.SetActive(false);
    }
}