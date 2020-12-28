using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused=false;
    public GameObject pausemenu,pausebutton;
   
    public void Pause()
    {
        isGamePaused = true;
        pausemenu.SetActive(true);
        pausebutton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isGamePaused = false;
        pausemenu.SetActive(false);
        pausebutton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
