using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
  

    public void reloadMiniGame()
    {
        SceneManager.LoadScene("Fire_Step4_S3");
    }
   
}
