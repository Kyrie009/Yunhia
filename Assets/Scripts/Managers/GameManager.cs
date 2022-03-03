using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public enum GameState { Start, Playing, Paused, GameOver}
public class GameManager : Singleton<GameManager>
{
    //enums
    public GameState gameState;

    //GameStates
    public void ChangeGameState(GameState _gamestate)
    {
        gameState = _gamestate;
        SetGameState();
    }
    public void SetGameState()
    {
        switch (gameState)
        {
            case GameState.Start:
                Cursor.visible = true;
                Time.timeScale = 1;
                break;
            case GameState.Playing:
                Cursor.visible = false;
                Time.timeScale = 1;
                break;
            case GameState.Paused:
                Cursor.visible = true;
                Time.timeScale = 0;
                break;
            case GameState.GameOver:
                Cursor.visible = true;
                Time.timeScale = 0;
                break;
        }
    }

    private void OnEnable()
    {
        GameEvents.OnStart += ChangeGameState;
        GameEvents.OnPlaying += ChangeGameState;
        GameEvents.OnPause += ChangeGameState;
        GameEvents.OnGameOver += ChangeGameState;
    }
    private void OnDisable()
    {
        GameEvents.OnStart -= ChangeGameState;
        GameEvents.OnPlaying -= ChangeGameState;
        GameEvents.OnPause -= ChangeGameState;
        GameEvents.OnGameOver -= ChangeGameState;
    }
}
