using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventPlayerActive : UnityEvent<GameObject> { }

public class PlayerManager : MonoBehaviour
{

    public bool playerIsActive;
    public GameObject playerPrefab;
    public GameObject spawnPoint;

    public GameObject playerObject;

    public EventPlayerActive OnPlayerIsActiveChanged = new EventPlayerActive();

    private void Start()
    {
        CheckPlayerActive();
        FindSpawnPoint();
    }

    public void FindSpawnPoint()
    {
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    public void SpawnPlayer()
    {
        CheckPlayerActive();
        FindSpawnPoint();

        if (spawnPoint != null)
        {
            if (!playerIsActive)
            {
                playerIsActive = true;
                playerObject = Instantiate(playerPrefab, spawnPoint.transform.position, playerPrefab.transform.rotation);
                GameManager.Instance.UpdateGameState(GameManager.GameState.RUNNING);

                if (OnPlayerIsActiveChanged != null)
                {
                    OnPlayerIsActiveChanged.Invoke(playerObject);
                }
            }
            else
            {
                print("Player Already active!");
            }
        }
        else
        {
            print("Spawn Point can't be found!");
        }


    }

    public void CheckPlayerActive()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            playerIsActive = true;
        }
        else
        {
            playerIsActive = false;
        }
    }


}
