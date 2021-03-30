using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// public class EventPlayerState : UnityEvent<GameObject, PlayerManager.PlayerState, PlayerManager.PlayerState> { }

public class PlayerManager : MonoBehaviour
{
    // public SO_SessionData sessionData;

    // public GameObject playerPrefab;
    // public GameObject spawnPoint;
    // public float spawnDelay;
    // public GameObject playerObject;

    // private void Start()
    // {
    //     sessionData.OnGameStateChanged.AddListener(HandleGameStateChanged);
    //     sessionData.OnPlayerStateChanged.AddListener(HandlePlayerStateChanged);

    // }

    // public void FindSpawnPoint()
    // {
    //     spawnPoint = GameObject.Find("SpawnPoint");
    // }

    // public IEnumerator SpawnPlayer()
    // {
    //     FindSpawnPoint();
    //     CheckPlayerActive();
    //     yield return new WaitForSecondsRealtime(spawnDelay);

    //     if (spawnPoint != null)
    //     {
    //         if (sessionData.CurrentPlayerState == SO_SessionData.PlayerState.SPAWNING)
    //         {

    //             playerObject = Instantiate(playerPrefab, spawnPoint.transform.position, playerPrefab.transform.rotation);
    //             sessionData.UpdatePlayerState(SO_SessionData.PlayerState.ACTIVE);

    //         }
    //         else
    //         {
    //             print("PlayerState must be set to SPAWNING to spawn player. Current Player State is: " + sessionData.CurrentPlayerState);
    //         }
    //     }
    //     else
    //     {
    //         print("Spawn Point can't be found!");
    //     }

    // }


    // public void CheckPlayerActive()
    // {
    //     if (GameObject.FindGameObjectWithTag("Player") != null)
    //     {
    //         playerObject = GameObject.FindGameObjectWithTag("Player");
    //         sessionData.UpdatePlayerState(SO_SessionData.PlayerState.ACTIVE);
    //     }
    // }


    //On playerstate change to spawning:
    //Open Portal
    //Instantiate player
    //Close Portal
    //Set playerstate to active
    //Set gamestate to running?

    // public void UpdatePlayerState(PlayerState state)
    // {
    //     previousPlayerState = currentPlayerState;
    //     currentPlayerState = state;

    //     switch (currentPlayerState)
    //     {
    //         case PlayerState.INACTIVE:

    //             break;

    //         case PlayerState.SPAWNING:
    //             StartCoroutine(SpawnPlayer());
    //             break;

    //         case PlayerState.ACTIVE:
    //             GameManager.Instance.sessionData.timerIsActive = true;
    //             break;

    //         case PlayerState.DESPAWNING:

    //             break;

    //     }

    //     print("Current PlayerState: " + currentPlayerState);
    //     if (OnPlayerStateChanged != null)
    //     {
    //         OnPlayerStateChanged.Invoke(playerObject, currentPlayerState, previousPlayerState);
    //     }
    // }

    // public void HandleGameStateChanged(SO_SessionData.GameState currentGameState, SO_SessionData.GameState previousGameState)
    // {
    //     switch (currentGameState)
    //     {
    //         case SO_SessionData.GameState.PREGAME:

    //             break;

    //         case SO_SessionData.GameState.RUNNING:
    //             if (previousGameState == SO_SessionData.GameState.LEVELSTART)
    //                 sessionData.UpdatePlayerState(SO_SessionData.PlayerState.SPAWNING);
    //             break;

    //         case SO_SessionData.GameState.PAUSED:

    //             break;

    //         case SO_SessionData.GameState.LEVELSTART:

    //             break;

    //         case SO_SessionData.GameState.LEVELEND:

    //             break;

    //         default:

    //             break;
    //     }
    // }

    // public void HandlePlayerStateChanged(SO_SessionData.PlayerState currentPlayerState, SO_SessionData.PlayerState previousPlayerState)
    // {
    //     switch (currentPlayerState)
    //     {
    //         case SO_SessionData.PlayerState.INACTIVE:

    //             break;

    //         case SO_SessionData.PlayerState.SPAWNING:
    //             StartCoroutine(SpawnPlayer());
    //             break;

    //         case SO_SessionData.PlayerState.ACTIVE:
                
    //             break;

    //         case SO_SessionData.PlayerState.DESPAWNING:

    //             break;

    //         default:

    //             break;
    //     }
    // }
}
