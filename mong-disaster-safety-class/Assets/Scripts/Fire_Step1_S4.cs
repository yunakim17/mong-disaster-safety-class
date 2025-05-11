using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Step1_S4 : MonoBehaviour
{
    public GameObject fuelImg, heatImg, airImg;
    public DialogueManager dialogueManager;

    private HashSet<int> visitedIndices = new HashSet<int>();
    private bool nextButtonActivated = false;

    public void showFuelImg()
    {
        fuelImg.SetActive(true);
        dialogueManager.ShowSingleLine(1);
        MarkVisited(1);
    }

    public void showHeatImg()
    {
        heatImg.SetActive(true);
        StartCoroutine(ShowTwoLines());
    }

    public void showAirImg()
    {
        airImg.SetActive(true);
        dialogueManager.ShowSingleLine(4);
        MarkVisited(4);
    }

    private void MarkVisited(int index)
    {
        Debug.Log($"MarkVisited 실행 중인 객체 이름: {gameObject.name}");

        if (visitedIndices.Add(index))
        {
            Debug.Log($"방문됨: {index}");
        }

        Debug.Log($"현재 방문된 수: {visitedIndices.Count}");

        if (!nextButtonActivated && visitedIndices.Count >= 4)
        {
            Debug.Log("모든 버튼 클릭 완료! nextButton 활성화 및 다음 대사 인덱스로 이동");
            nextButtonActivated = true;
            dialogueManager.SetCurrentLineIndex(4);
            dialogueManager.nextButton.SetActive(true);
        }
        else
        {
            dialogueManager.nextButton.SetActive(false);
        }
    }

    private IEnumerator ShowTwoLines()
    {
        Debug.Log("ShowTwoLines() 시작");

        yield return StartCoroutine(dialogueManager.ShowSingleLineAndWait(2));
        MarkVisited(2);

        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(dialogueManager.ShowSingleLineAndWait(3));
        MarkVisited(3);
    }
}


