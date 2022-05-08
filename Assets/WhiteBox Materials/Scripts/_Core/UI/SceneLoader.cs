using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : GameBehaviour
{
    public void LoadNewGame() //Sets everything for a fresh run of a new game.
    {
        _GM.isFirstRun = true;
        _AM.GetNewGameBGM();
        GameEvents.ReportGamePlaying();
        SceneManager.LoadScene("Runic Forest");
    }
    //type in the scene you want loaded
    public void LoadScene(string _sceneName)
    {
        _AM.GetButtonSound();
        GameEvents.ReportGamePlaying();
        SceneManager.LoadScene(_sceneName);
    }
    //Reloads the current scene we are in
    public void ReloadScene()
    {
        _GM.isFirstRun = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void LoadTitle()
    {       
        _AM.GetTitleBGM();
        _AM.GetButtonSound();
        GameEvents.ReportGameStart();
        SceneManager.LoadScene("Title");
    }

    //Quits our game
    public void QuitGame()
    {
        _AM.GetButtonSound();
        Application.Quit();
    }
}
