using System;

//Gets Reports and Broadcasts them
public static class GameEvents
{
    //GameStates
    public static event Action<GameState> OnStart = null;
    public static event Action<GameState> OnPlaying = null;
    public static event Action<GameState> OnPause = null;
    public static event Action<GameState> OnGameOver = null;
    //Enemy
    public static event Action<Enemy> OnEnemyDied = null;
    //Timer related stuff
    public static event Action OnCorruptionStart = null;
    public static event Action OnCorruptionStop = null;
    public static event Action OnCorrupted = null;
    //Player
    public static event Action<Player> OnPlayerDied = null;
    public static event Action<bool> OnFreezeEvent = null;

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
    //Enemy Status
    public static void ReportEnemyDied(Enemy _enemy)
    {
        OnEnemyDied?.Invoke(_enemy);
    }
    //Death Timer
    public static void ReportCorrupted()
    {
        OnCorrupted?.Invoke();
    }
    public static void ReportCorruptionStart()
    {
        OnCorruptionStart?.Invoke();
    }
    public static void ReportCorruptionStop()
    {
        OnCorruptionStop?.Invoke();
    }
    //Player Status
    public static void ReportPlayerDied(Player _player)
    {
        OnPlayerDied?.Invoke(_player);
    }
    public static void ReportFreezeEvent(bool _fStatus)
    {
        OnFreezeEvent?.Invoke(_fStatus);
    }

}