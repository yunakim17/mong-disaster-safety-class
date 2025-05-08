using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showImgs_Fire : MonoBehaviour
{
    public GameObject img1, img2, img3;

    public void show1Img()
    {
        img1.SetActive(true);

        img2.SetActive(false);
        img3.SetActive(false);
        
    }

    public void show2Img()
    {
        img2.SetActive(true);

        img1.SetActive(false);
        img3.SetActive(false);
    }

    public void show3Img()
    {
        img3.SetActive(true);

        img1.SetActive(false);
        img2.SetActive(false);
        img2.SetActive(false);
    }

  
}
