using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public static bool GameIsPaused = false;
    public static bool RestartGame = false;
    public GameObject pauseMenuUI;
    [SerializeField]
    public static bool isPaused;
    public static bool gameOver = false;



    private void Awake()
    {
        gameOver = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

    }

   

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        isPaused = false;
    }

    public void GameOver()
    {
        GameIsPaused = true;
        isPaused = true;
    }

    public void Pause()
    { 
        GameIsPaused = true;
        isPaused = true;

        if (!gameOver)
        {
            pauseMenuUI.SetActive(true);

        }

        //  Debug.Log("PAUSE MENU...");
    }

    public void LoadMenu()
    {
        Debug.Log("Loading...");
    }

    public void Restart()
    {
        isPaused = false;
        GameIsPaused = false;
        if (gameOver)
        { 
            gameOver = false; 
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quiting...");

    }



}
