using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire4_TimeBar : MonoBehaviour
{
    public static Fire4_TimeBar Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Slider timerSlider;
    public float totalTime = 10f;
    public Image fillImage;
    private float timeLeft;
    public GameObject gameOverPanel;

    public bool isRunning = false;
  

    Color Mygreen;
    Color Myred;

    void Start()
    {
        timeLeft = totalTime;
        timerSlider.maxValue = totalTime;
        timerSlider.value = totalTime;
        isRunning = false;

    ColorUtility.TryParseHtmlString("#7EFF6D", out Mygreen);
        ColorUtility.TryParseHtmlString("#FF4A45", out Myred);

        gameOverPanel.SetActive(false);
    }

    public void timerStart()
    {
        isRunning = true;
    }



    void Update()
    {
        if (isRunning) { 
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerSlider.value = timeLeft;

            float t = 1 - (timeLeft / totalTime); // 0~1
            fillImage.color = Color.Lerp(Mygreen, Myred, t);
        }
        else if (timeLeft <= 0) 
        {
            timerSlider.value = 0f;
            gameOverPanel.SetActive(true);

        }


        //Å¸ÀÌ¸Ó¹Ù »ö»ó º¯°æ
        float progress = 1 - (timeLeft / totalTime);

        if (progress < 0.5f)
        {
            // ÃÊ·Ï ¡æ ³ë¶û
            float t = progress / 0.5f;
            fillImage.color = Color.Lerp(Mygreen, Color.yellow, t); 
        }
        else
        {
            // ³ë¶û ¡æ »¡°­
            float t = (progress - 0.5f) / 0.5f;
            fillImage.color = Color.Lerp(Color.yellow, Myred, t);
        }

        }
    }
}
