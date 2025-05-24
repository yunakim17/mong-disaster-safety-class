using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showImgs : MonoBehaviour
{
    public GameObject img12, img34, img56, img7;

    public void show12Img()
    {
        img12.SetActive(true);

        img34.SetActive(false);
        img56.SetActive(false);
        img7.SetActive(false);
    }

    public void show34Img()
    {
        img34.SetActive(true);

        img12.SetActive(false);
        img56.SetActive(false);
        img7.SetActive(false);
    }

    public void show56Img()
    {
        img56.SetActive(true);

        img34.SetActive(false);
        img12.SetActive(false);
        img7.SetActive(false);
    }

    public void show7Img()
    {
        img7.SetActive(true);

        img34.SetActive(false);
        img56.SetActive(false);
        img12.SetActive(false);
    }
}
