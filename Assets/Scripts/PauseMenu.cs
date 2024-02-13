using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPause;
    [SerializeField] GameObject optionsMenuPause;
    public static bool gameIsPaused = false;

    public void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        mainMenuPause.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        mainMenuPause.SetActive(true);
        gameIsPaused = true;
    }


    public void Restart()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        mainMenuPause.SetActive(false);
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        mainMenuPause.SetActive(false);
        SceneManager.LoadScene(1);
    }

     public void Options()
    {
        mainMenuPause.SetActive(false);
        optionsMenuPause.SetActive(true);
    }

    public void Back()
    {
        optionsMenuPause.SetActive(false);
        mainMenuPause.SetActive(true);
    }
}
