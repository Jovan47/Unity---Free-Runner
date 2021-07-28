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
        if (gameOver)
        {
            GameIsPaused = true;
        }

    }



    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }



    public void Pause()
    { 
        GameIsPaused = true;

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
        GameIsPaused = false;
        gameOver = false;
        StartCoroutine(WaitOneSec());
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator WaitOneSec()
    {
        yield return new WaitForSeconds(1);
        
    }

    public void QuitGame()
    {
        Debug.Log("Quiting...");

    }



}
