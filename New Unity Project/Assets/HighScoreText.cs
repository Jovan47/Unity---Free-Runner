using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreText : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    public float timer = 0.0f;
    public GameObject obj;
    public float tweenTime=0.9f;
    public float timerSecond=1f;
    private float timerThird = 0.0f;
    void Awake()
    {
        highScore.text= (0.0f).ToString("F2");


    }

    void Update()
    {
        timerThird += Time.deltaTime;
        if (!PauseMenu.GameIsPaused &&timerThird>5f)
        {
            timer += Time.deltaTime;
            highScore.text ="SCORE: " + timer.ToString("F2");

            timerSecond += Time.deltaTime;
            if (timerSecond >= 1)
            {
                timerSecond = 0;
                TweenObject();
            }
        }

    }

    public void TweenObject()
    {
        
            LeanTween.cancel(obj);
            transform.localScale = Vector3.one;
            LeanTween.scale(obj, Vector3.one * 1.1f, tweenTime).setEaseShake();
        
    }
}
