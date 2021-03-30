using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    public GameObject levelEndMenuUI;
    public GameObject gameEndMenuUI;

    public List<GameObject> targetsList = new List<GameObject>();
    public int targetCount;
    private string currentLevel;

    private TimeSpan timeSpan;
    public bool timerIsActive;
    public float timeElapsed;
    public string timerText;

    // private void Start()
    // {
    //     timerIsActive = false;
    //     ResetTimer();
    //     GameManager.Instance.OnLevelChanged.AddListener(HandleLevelChanged);
    //     GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    //     GameManager.Instance.playerManager.OnPlayerStateChanged.AddListener(HandlePlayerStateChanged);

    // }


    // public void BeginTimer()
    // {
    //     timerIsActive = true;
    // }

    // public void EndTimer()
    // {
    //     timerIsActive = false;
    // }

    // public void ResetTimer()
    // {
    //     timerIsActive = false;
    //     timeElapsed = 0f;
    //     timerText = "00:00.00";
    // }

    // public void HandleLevelChanged(string newLevel)
    // {
    //     currentLevel = newLevel;

    // }

    // private void Update()
    // {
    //     if (timerIsActive)
    //     {
    //         timeElapsed += Time.deltaTime;
    //         timeSpan = TimeSpan.FromSeconds(timeElapsed);
    //         timerText = timeSpan.ToString("mm':'ss'.'ff");
    //     }

    // }

    // public void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    // {
    //     switch (currentState)
    //     {
    //         case GameManager.GameState.PREGAME:

    //             break;

    //         case GameManager.GameState.RUNNING:

    //             break;

    //         case GameManager.GameState.PAUSED:

    //             break;

    //         case GameManager.GameState.LEVELSTART:

    //             BuildTargetsList();

    //             break;

    //         case GameManager.GameState.LEVELEND:

    //             break;

    //         default:

    //             break;
    //     }

    // }

    // public void HandlePlayerStateChanged(GameObject playerObject, PlayerManager.PlayerState currentPlayerState, PlayerManager.PlayerState previousPlayerState)
    // {
    //     switch (currentPlayerState)
    //     {
    //         case PlayerManager.PlayerState.INACTIVE:

    //             break;

    //         case PlayerManager.PlayerState.SPAWNING:


    //             break;

    //         case PlayerManager.PlayerState.ACTIVE:
    //             BeginTimer();
    //             break;

    //         case PlayerManager.PlayerState.DESPAWNING:

    //             break;

    //         default:

    //             break;
    //     }
    // }

    
    // public void CheckTargetsRemaining()
    // {
    //     targetCount = targetsList.Count;
    //     print(targetCount + " targets remaining");
    //     if (targetCount == 0)
    //     {
    //         GameManager.Instance.UpdateGameState(GameManager.GameState.LEVELEND);
    //     }
    // }
    

    // public void BuildTargetsList()
    // {
    //     ResetTargetsList();

    //     foreach (GameObject targetObject in GameObject.FindGameObjectsWithTag("Target"))
    //     {
    //         targetsList.Add(targetObject);
    //         print(targetObject.name);
    //     }

    //     targetCount = targetsList.Count;
    //     print(targetCount + " targets on the list");
    // }

    // public void ResetTargetsList()
    // {
    //     targetsList.Clear();
    //     targetCount = 0;
    // }

    // public void RemoveTarget(GameObject target)
    // {
    //     targetsList.Remove(target);
    //     CheckTargetsRemaining();
    // }

}


