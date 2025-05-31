using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenGameManager : MonoBehaviour
{
    public GameObject falsePanel;
    public GameObject correctPanel;
    public GameObject clearPanel;

    public GameObject faucet;
    public GameObject towel;
    public GameObject water;

    public Sprite towelImg2;
    public Sprite faucetImg2;

    public Vector3 towelPos;
    private bool foundTowel;
  
    void Start()
    {
        //SpriteRenderer srFaucet = faucet.GetComponent<SpriteRenderer>();
        //SpriteRenderer srTowel = towel.GetComponent<SpriteRenderer>();

        foundTowel = false;

        falsePanel.SetActive(false);
        correctPanel.SetActive(false);
        clearPanel.SetActive(false);

        water.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Fire4_TimeBar.Instance != null && Fire4_TimeBar.Instance.isRunning) // 타이머가 돌아갈 때만 오브젝트 터치가 가능하도록
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

            if (hit.collider != null)
            {
                string tag = hit.collider.tag;
                Debug.Log("터치된 태그: " + tag);

                if (hit.collider.CompareTag("towel") && !foundTowel)
                {
                    //수건 찾으면 수건 이미지 확대, 위치고정
                    hit.collider.gameObject.transform.position = towelPos;
                    hit.collider.gameObject.transform.localScale = new Vector2(hit.collider.transform.localScale.x * 2,
                        hit.collider.transform.localScale.y * 2);

                    //수건 찾았다 패널 보이기
                    Invoke("ShowCorrectPanel", 1f);
                    foundTowel = true; //1. 수건 찾음

                }
                else if (tag == "falseObj" && !foundTowel)
                {
                    falsePanel.SetActive(true);
                    Invoke("HideFalsePanel", 2.5f);
                }


                if (hit.collider.CompareTag("faucet") && foundTowel)
                {
                    //수도꼭지 이미지: 닫힘 -> 열림 으로 바꾸기
                    SpriteRenderer srFaucet = faucet.GetComponent<SpriteRenderer>();
                    srFaucet.sprite = faucetImg2;

                    //수도꼭지 물나오는 이미지 보이게
                    water.SetActive(true);

                    //수건 이미지 : 마른수건 -> 젖은수건 으로 바꾸기
                    SpriteRenderer srTowel = towel.GetComponent<SpriteRenderer>();
                    srTowel.sprite = towelImg2;


                    //미니게임 성공 패널 보이기 
                    Invoke("ShowClearPanel", 2f);

                    //타이머 바 멈추기
                    Fire4_TimeBar.Instance.isRunning = false;
                    
                    correctPanel.SetActive(false);


                }



            }
        }


    }


    public void HideFalsePanel()
    {
        falsePanel.SetActive(false);
    }

   public void ShowCorrectPanel()
    {
        correctPanel.SetActive(true);
    }

    public void ShowClearPanel()
    {
        clearPanel.SetActive(true);
    }

}

