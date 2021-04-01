using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventGameState : UnityEvent<SessionDataSO.GameState, SessionDataSO.GameState> { }
public class EventPlayerState : UnityEvent<SessionDataSO.PlayerState, SessionDataSO.PlayerState> { }

[CreateAssetMenu(fileName = "SessionData", menuName = "Scriptable Objects/SessionData")]
public class SessionDataSO : ScriptableObject

{
    // public int currentLevelSetIndex;
    public WorldDatabaseSO worldDatabase;

    // public string currentLevelName;
    // public string nextLevelName;

    public float levelTimer;
    public bool timerIsActive;

    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED,
        LEVELSTART,
        LEVELEND,
    }

    public GameState currentGameState;
    public GameState CurrentGameState
    {
        get { return currentGameState; }
        private set { currentGameState = value; }
    }

    private GameState previousGameState;

    public EventGameState OnGameStateChanged = new EventGameState();


    public enum PlayerState
    {
        INACTIVE,
        SPAWNING,
        ACTIVE,
        DESPAWNING,
    }

    public PlayerState currentPlayerState;
    public PlayerState CurrentPlayerState
    {
        get { return currentPlayerState; }
        private set { currentPlayerState = value; }
    }

    private PlayerState previousPlayerState;

    public EventPlayerState OnPlayerStateChanged = new EventPlayerState();



    public void ResetTimer()
    {
        timerIsActive = false;
        levelTimer = 0f;
    }

    // // sets the index of the current level within the current level set
    // public void SetCurrentLevelSetIndex(int i)
    // {
    //     currentLevelSetIndex = i;
    // }

    public void UpdateGameState(GameState state)
    {
        previousGameState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 0f;
                break;

            case GameState.RUNNING:
                Time.timeScale = 1f;
                break;

            case GameState.PAUSED:
                Time.timeScale = 0f;
                break;

            case GameState.LEVELSTART:
                Time.timeScale = 0f;
                break;

            case GameState.LEVELEND:
                Time.timeScale = 0f;
                break;

            default:
                Time.timeScale = 0f;
                break;
        }

        Debug.Log("Current GameState: " + currentGameState);
        if (OnGameStateChanged != null)
        {
            OnGameStateChanged.Invoke(currentGameState, previousGameState);
        }

    }

    public void UpdatePlayerState(PlayerState state)
    {
        previousPlayerState = currentPlayerState;
        currentPlayerState = state;

        switch (currentPlayerState)
        {
            case PlayerState.INACTIVE:

                break;

            case PlayerState.SPAWNING:
                
                break;

            case PlayerState.ACTIVE:
                timerIsActive = true;
                break;

            case PlayerState.DESPAWNING:
                timerIsActive = false;
                break;

        }

        Debug.Log("Current PlayerState: " + currentPlayerState);
        if (OnPlayerStateChanged != null)
        {
            OnPlayerStateChanged.Invoke(currentPlayerState, previousPlayerState);
        }
    }

    public void ResetSessionData()
    {
        ResetTimer();
        // currentLevelName = string.Empty;
        // nextLevelName = string.Empty;
        currentPlayerState = PlayerState.INACTIVE;
        // currentLevelSetIndex = 0;
    }

}
