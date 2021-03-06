using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreKeeper : MonoBehaviour
{
    public GameObject levelEndMenuUI;
    public GameObject gameEndMenuUI;

    public GameObject[] targets;
    public int targetCount;
    private string currentLevel;

    public float currentTimer;

    private void Start()
    {
        currentTimer = 0;
        GameManager.Instance.OnLevelChanged.AddListener(HandleLevelChanged);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        CheckTargetsRemaining();
    }

    public void HandleLevelChanged(string newLevel)
    {
        currentLevel = newLevel;

    }

    public void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        switch (currentState)
        {
            case GameManager.GameState.PREGAME:
                ResetTargetsRemaining();
                break;

            case GameManager.GameState.RUNNING:

                break;

            case GameManager.GameState.PAUSED:

                break;

            case GameManager.GameState.LEVELSTART:

                CheckTargetsRemaining();
                break;

            case GameManager.GameState.LEVELEND:

                break;

            default:

                break;
        }


    }

    public void CheckTargetsRemaining()
    {
        ResetTargetsRemaining();
        targets = GameObject.FindGameObjectsWithTag("Target");
        targetCount = targets.Length;
        print(targetCount + " targets remaining");
    }

    public void PrintTargetsRemaining()
    {
        print(targetCount + " targets remaining");
        if(targetCount == 0)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.LEVELEND);
        }
    }

    public void ResetTargetsRemaining()
    {
        targetCount = 0;
    }

}


