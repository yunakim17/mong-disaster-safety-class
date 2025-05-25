using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class Scene4story : MonoBehaviour
{
    public GameObject nextButton;
    public GameObject miniGame2;
    public GameObject miniGame22;

    public GameObject puzzleImageSplitter;
    public GameObject puzzleManager;
    public GameObject puzzlePiece;
    public GameObject puzzleSlots;

    public GameObject backgroundOverlay;

    private bool puzzleFinished = false;

    void Start()
    {
        nextButton.SetActive(false);
        miniGame2.SetActive(true);
        miniGame22.SetActive(true);

        puzzleImageSplitter.SetActive(false);
        puzzleManager.SetActive(false);
        puzzlePiece.SetActive(false);
        puzzleSlots.SetActive(false);

        backgroundOverlay.SetActive(true);

        if (nextButton != null)
        {
            Button btn = nextButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(OnNextClicked);
            }
        }
    }

    public void OnNextClicked()
    {
        Debug.Log("Next 버튼 클릭됨");

        if (puzzleFinished)
        {
            SceneManager.LoadScene("Eq_Step4_S4-B");
        }
    }

    public void OnStartMiniGame()
    {
        Debug.Log("시작하기 클릭됨 - 퍼즐 표시");

        miniGame2.SetActive(false);
        miniGame22.SetActive(false);

        puzzleImageSplitter.SetActive(true);
        puzzleManager.SetActive(true);
        puzzlePiece.SetActive(true);
        puzzleSlots.SetActive(true);
    }

    public void OnPuzzleComplete()
    {
        Debug.Log("퍼즐 완료 후 Next 버튼만 표시");

        puzzleFinished = true;

        if (nextButton != null)
            nextButton.SetActive(true);
    }
}
