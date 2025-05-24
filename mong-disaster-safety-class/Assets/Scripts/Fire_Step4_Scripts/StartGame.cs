using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject startPanel;

    private void Start()
    {
        startPanel.SetActive(true);
    }

    public void StartBtnPressed()
    {
        Fire4_TimeBar.Instance.timerStart();
        startPanel.SetActive(false);

    }
}
