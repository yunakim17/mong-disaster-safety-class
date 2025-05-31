using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    //public static DragDrop Instance;
    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //    }
    //}


    public GameObject correct1_frame;
    public GameObject correct2_frame;
    public GameObject correct3_frame;

    public float Dropdistance;

    private Vector2 OriginPos;
    private bool isDragging = false;

    void Start()
    {
        gameObject.SetActive(false);//선택지 패널 안보이게

        //선택지 매치되는 프레임도 안보이게
        correct1_frame.SetActive(false);
        correct2_frame.SetActive(false);
        correct3_frame.SetActive(false); 

        OriginPos = gameObject.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && MatchGameManager.Instance.isStarted)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(mousePos))
            {
                isDragging = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && MatchGameManager.Instance.isStarted)
        {
            if (isDragging)
            {
                isDragging = false;
                DropObject();
            }
        }

        if (isDragging && MatchGameManager.Instance.isStarted)
        {
            DragObject();
        }
    }

    public void DragObject()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        gameObject.transform.position = mousePos;
    }

    public void DropObject()
    {
        float distance1 = Vector3.Distance(gameObject.transform.position, correct1_frame.transform.position);
        float distance2 = Vector3.Distance(gameObject.transform.position, correct2_frame.transform.position);
        float distance3 = Vector3.Distance(gameObject.transform.position, correct3_frame.transform.position);

        // 첫번째 선택지 - 프레임 연결
        if (distance1 < Dropdistance)
        {
            if (MatchGameManager.Instance.line1_Matched)
            {
                Debug.Log("1번 영역은 이미 맞췄음. 무시!");
                gameObject.transform.position = OriginPos;
                return;
            }

            if (gameObject.CompareTag("crt1"))
            {
                gameObject.transform.position = correct1_frame.transform.position;
                Debug.Log("1번: 올바른 선택지고 순서가 맞아!!");
                MatchGameManager.Instance.line1_Matched = true;
                return;
            }
            else if (gameObject.CompareTag("crt2") || gameObject.CompareTag("crt3"))
            {
                Debug.Log("1번: 올바른 선택지지만 순서가 잘못됐어!!");
                MatchGameManager.Instance.showWrongOrderPanel();
                gameObject.transform.position = OriginPos;
                return;
            }
            else
            {
                Debug.Log("1번: 잘못된 선택지야!!");
                MatchGameManager.Instance.showWrongAnswerPanel();
                gameObject.transform.position = OriginPos;
                return;
            }
        }

        // 두번째 선택지 - 프레임 연결
        if (distance2 < Dropdistance)
        {
            if (MatchGameManager.Instance.line2_Matched)
            {
                Debug.Log("2번 영역은 이미 맞췄음. 무시!");
                gameObject.transform.position = OriginPos;
                return;
            }

            if (gameObject.CompareTag("crt2"))
            {
                gameObject.transform.position = correct2_frame.transform.position;
                Debug.Log("2번: 올바른 선택지고 순서가 맞아!!");
                MatchGameManager.Instance.line2_Matched = true;
                return;
            }
            else if (gameObject.CompareTag("crt1") || gameObject.CompareTag("crt3"))
            {
                Debug.Log("2번: 올바른 선택지지만 순서가 잘못됐어!!");
                MatchGameManager.Instance.showWrongOrderPanel();
                gameObject.transform.position = OriginPos;
                return;
            }
            else
            {
                Debug.Log("2번: 잘못된 선택지야!!");
                MatchGameManager.Instance.showWrongAnswerPanel();
                gameObject.transform.position = OriginPos;
                return;
            }
        }

        // 세번째 선택지 - 프레임 연결
        if (distance3 < Dropdistance)
        {
            if (MatchGameManager.Instance.line3_Matched)
            {
                Debug.Log("3번 영역은 이미 맞췄음. 무시!");
                gameObject.transform.position = OriginPos;
                return;
            }

            if (gameObject.CompareTag("crt3"))
            {
                gameObject.transform.position = correct3_frame.transform.position;
                Debug.Log("3번: 올바른 선택지고 순서가 맞아!!");
                MatchGameManager.Instance.line3_Matched = true;
                return;
            }
            else if (gameObject.CompareTag("crt1") || gameObject.CompareTag("crt2"))
            {
                Debug.Log("3번: 올바른 선택지지만 순서가 잘못됐어!!");
                MatchGameManager.Instance.showWrongOrderPanel();
                gameObject.transform.position = OriginPos;
                return;
            }
            else
            {
                Debug.Log("3번: 잘못된 선택지야!!");
                MatchGameManager.Instance.showWrongAnswerPanel();
                gameObject.transform.position = OriginPos;
                return;
            }
        }

        // 어느 선택지 프레임과도 거리가 먼 경우
        Debug.Log("너무 멀리 드랍했어!");
        gameObject.transform.position = OriginPos;
    }




}
