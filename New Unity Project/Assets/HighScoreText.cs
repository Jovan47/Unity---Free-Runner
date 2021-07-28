using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreText : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI highScoreText;

    public Animator animator;
    public float timer = 0.0f;
    public GameObject obj;
    public float tweenTime=0.9f;
    public float timerSecond=1f;
    private float timerThird = 0.0f;

    private float highScore = 0.0f;

    void Awake()
    {   
        Score.text= (0.0f).ToString("F2");
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreText.text ="HighScore: " + PlayerPrefs.GetFloat("HighScore").ToString("F2");
            highScore = PlayerPrefs.GetFloat("HighScore");
        }

    }

    void Update()
    {
        timerThird += Time.deltaTime;
        if (!PauseMenu.GameIsPaused &&timerThird>5f && !PauseMenu.gameOver)
        {
            timer += Time.deltaTime;
            timerSecond += Time.deltaTime;
            Score.text ="SCORE: " + timer.ToString("F2");
            if (timerSecond >= 1)
            {
                timerSecond = 0;
                TweenObject();
            }
        }

        if(timer>highScore)
        {
            highScore = timer;
        }

        if (PauseMenu.gameOver)
        {
            animator.SetTrigger("GameOver");
            float highS=0;

            if (PlayerPrefs.HasKey("HighScore"))
            {
                 highS = PlayerPrefs.GetFloat("HighScore");
            }

            if (highS < timer)
            {
                PlayerPrefs.SetFloat("HighScore", timer);
                PlayerPrefs.Save();
                highScoreText.text = "HighScore: " + timer.ToString("F2");
            }
            else
            {
                highScoreText.text = "HighScore: " + highS.ToString("F2");

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
