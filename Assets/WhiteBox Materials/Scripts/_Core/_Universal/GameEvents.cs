using System;

//Gets Reports and Broadcasts them
public static class GameEvents
{
    //GameStates
    public static event Action<GameState> OnStart = null;
    public static event Action<GameState> OnPlaying = null;
    public static event Action<GameState> OnPause = null;
    public static event Action<GameState> OnGameOver = null;
    public static event Action<GameState> OnInteracting = null;

    //Player
    public static event Action<Player> OnPlayerDied = null;

    //functions reported to by the scripts
    //GameStates
    public static void ReportGameStart()
    {
        OnStart?.Invoke(GameState.Start);
    }
    public static void ReportGamePlaying()
    {
        OnPlaying?.Invoke(GameState.Playing);
    }
    public static void ReportGamePause()
    {
        OnPause?.Invoke(GameState.Paused);
    }
    public static void ReportGameOver()
    {
        OnGameOver?.Invoke(GameState.GameOver);
    }
    public static void ReportInteracting()
    {
        OnInteracting?.Invoke(GameState.Interacting);
    }

    //Player Status
    public static void ReportPlayerDied(Player _player)
    {
        OnPlayerDied?.Invoke(_player);
    }


}