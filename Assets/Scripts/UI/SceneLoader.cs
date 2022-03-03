using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : GameBehaviour
{
    public AudioSource button;
    //Will Change Scene to the string passed in
    public void ChangeScene(string _sceneName)
    {
        button.Play();
        SceneManager.LoadScene(_sceneName);
    }
    //Reloads the current scene we are in
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    //Loads Title using string name
    public void ToTitleScene()
    {       
        GameEvents.ReportGameStart();
        SceneManager.LoadScene(0);
    }
    //Loads First GameLevel
    //Loads Title using string name
    public void StartingScene()
    {
        button.Play();
        GameEvents.ReportGamePlaying();
        SceneManager.LoadScene(1);
    }

    //Gets Active Scene Name
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //Quits our game
    public void QuitGame()
    {
        button.Play();
        Application.Quit();
    }
}
