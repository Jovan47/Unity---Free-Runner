using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool RestartGame = false;
    public GameObject pauseMenuUI;
    public static bool isPaused;

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
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        isPaused = true;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quiting...");

    }



}
