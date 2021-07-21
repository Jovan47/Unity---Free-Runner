using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    private bool loadNextScene=false;
    public Animator transition;
    public float transitionTime = 2f;
    

    void Update()
    {
        if (loadNextScene)
        {
            StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex+1));
        }

    }

    IEnumerator LoadNextLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

     public void LoadNextScene()
    {
        loadNextScene = true;
    }

}
