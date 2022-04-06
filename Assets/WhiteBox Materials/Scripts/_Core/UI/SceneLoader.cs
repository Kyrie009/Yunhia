using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : GameBehaviour
{
    public AudioSource button;
    //type in the scene you want loaded
    public void LoadScene(string _sceneName)
    {
        button.Play();
        GameEvents.ReportGamePlaying();
        SceneManager.LoadScene(_sceneName);
    }
    //Reloads the current scene we are in
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void LoadTitle()
    {
        button.Play();
        GameEvents.ReportGameStart();
        SceneManager.LoadScene("Title");
    }

    //Quits our game
    public void QuitGame()
    {
        button.Play();
        Application.Quit();
    }
}
