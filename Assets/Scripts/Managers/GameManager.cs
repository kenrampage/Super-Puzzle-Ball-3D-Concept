using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class EventLevelData : UnityEvent<string> { }


public class GameManager : Singleton<GameManager>
{
    public SessionDataSO sessionData;
    public WorldDatabaseSO worldDatabase;

    // public GameObject[] systemPrefabs;
    // private List<GameObject> instancedSystemPrefabs;

    // public EventLevelData OnLevelChanged = new EventLevelData();


    public UIManager uiManager;

    private InputActions inputActions;

    public List<GameObject> targetsList = new List<GameObject>();
    public int targetCount;

    public GameObject playerPrefab;
    public float spawnDelay;

    public GameObject playerObject;
    [HideInInspector] public GameObject spawnPoint;

    override public void Awake()
    {
        base.Awake();
        inputActions = new InputActions();
        worldDatabase.UpdateIndexes();
        // DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        // instancedSystemPrefabs = new List<GameObject>();

        // signs up for events
        sessionData.OnGameStateChanged.AddListener(HandleGameStateChanged);
        sessionData.OnPlayerStateChanged.AddListener(HandlePlayerStateChanged);
        inputActions.UI.ToggleMenu.performed += _ => ToggleGamePaused();

        // reset state machines
        //  sessionData.UpdateGameState(SessionDataSO.GameState.LEVELSTART);
        // sessionData.UpdatePlayerState(SessionDataSO.PlayerState.INACTIVE);

        InitializeLevel();
        // development only
        FindSpawnPoint();
        


    }

    private void Update()
    {
        if (sessionData.timerIsActive)
        {
            sessionData.levelTimer += Time.deltaTime;
        }
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    // instantiates system prefabs from the serialized list
    // void InstantiateSystemPrefabs()
    // {
    //     GameObject prefabInstance;

    //     for (int i = 0; i < systemPrefabs.Length; i++)
    //     {
    //         prefabInstance = Instantiate(systemPrefabs[i]);
    //         instancedSystemPrefabs.Add(prefabInstance);
    //     }
    // }

    // when game manager is destroyed also destroy all system prefabs on the list
    // protected override void OnDestroy()
    // {
    //     base.OnDestroy();

    //     for (int i = 0; i < instancedSystemPrefabs.Count; i++)
    //     {
    //         Destroy(instancedSystemPrefabs[i]);
    //     }

    //     instancedSystemPrefabs.Clear();

    // }

    // toggles paused/running gamestate
    private void ToggleGamePaused()
    {
        if (sessionData.CurrentGameState == SessionDataSO.GameState.RUNNING)
        {
            sessionData.UpdateGameState(SessionDataSO.GameState.PAUSED);
        }
        else if (sessionData.CurrentGameState == SessionDataSO.GameState.PAUSED)
        {
            sessionData.UpdateGameState(SessionDataSO.GameState.RUNNING);
        }

    }



    // function to state the level from a UI button
    public void StartLevel()
    {
        if (sessionData.CurrentGameState == SessionDataSO.GameState.LEVELSTART)
        {
            sessionData.UpdateGameState(SessionDataSO.GameState.RUNNING);
        }
        else
        {
            print("Level not loaded or GameState isn't LEVELSTART");
        }

    }

    // searches for gameobjects tagged as "Target" and adds them to a list
    public void BuildTargetsList()
    {
        ResetTargetsList();

        foreach (GameObject targetObject in GameObject.FindGameObjectsWithTag("Target"))
        {
            targetsList.Add(targetObject);
        }

        targetCount = targetsList.Count;
        print(targetsList.Count + " objects on the Targets list");
    }

    // Clears the target list
    public void ResetTargetsList()
    {
        targetsList.Clear();
        targetCount = 0;
    }

    // Removes specified target from target list and updates the
    public void RemoveTarget(GameObject target)
    {
        targetsList.Remove(target);
        CheckTargetsRemaining();
    }

    // Checks how many targets are left on the list. If 0 update gamestate to LEVELEND
    public void CheckTargetsRemaining()
    {
        targetCount = targetsList.Count;
        print(targetCount + " targets remaining");
        if (targetCount == 0)
        {
            sessionData.UpdateGameState(SessionDataSO.GameState.LEVELEND);
        }
    }

    // Statemachine for GameState
    public void HandleGameStateChanged(SessionDataSO.GameState currentGameState, SessionDataSO.GameState previousGameState)
    {
        switch (currentGameState)
        {
            case SessionDataSO.GameState.PREGAME:

                break;

            case SessionDataSO.GameState.RUNNING:
                // Spawns player if the game level is running initially
                if (previousGameState == SessionDataSO.GameState.LEVELSTART)
                    sessionData.UpdatePlayerState(SessionDataSO.PlayerState.SPAWNING);
                break;

            case SessionDataSO.GameState.PAUSED:

                break;

            case SessionDataSO.GameState.LEVELSTART:
                sessionData.ResetTimer();

                break;

            case SessionDataSO.GameState.LEVELEND:
                sessionData.UpdatePlayerState(SessionDataSO.PlayerState.DESPAWNING);
                break;

            default:

                break;
        }

    }

    public void HandlePlayerStateChanged(SessionDataSO.PlayerState currentPlayerState, SessionDataSO.PlayerState previousPlayerState)
    {
        switch (currentPlayerState)
        {
            case SessionDataSO.PlayerState.INACTIVE:
                if(playerObject != null)
                {
                    Destroy(playerObject);
                }
                break;

            case SessionDataSO.PlayerState.SPAWNING:
                StartCoroutine(SpawnPlayer());
                break;

            case SessionDataSO.PlayerState.ACTIVE:
                break;

            case SessionDataSO.PlayerState.DESPAWNING:

                break;

            default:

                break;
        }
    }

    // Searches the level for the spawnpoint object. Might want to change this to a tag.
    public void FindSpawnPoint()
    {
        spawnPoint = GameObject.Find("SpawnPoint");
    }

    // spawns the player at the spawnpoint after the specified delay so it can match up with the portal animation. Then updates player state
    public IEnumerator SpawnPlayer()
    {
        FindSpawnPoint();
        yield return new WaitForSecondsRealtime(spawnDelay);

        if (spawnPoint != null && playerObject == null)
        {
            if (sessionData.CurrentPlayerState == SessionDataSO.PlayerState.SPAWNING)
            {

                playerObject = Instantiate(playerPrefab, spawnPoint.transform.position, playerPrefab.transform.rotation);
                sessionData.UpdatePlayerState(SessionDataSO.PlayerState.ACTIVE);

            }
            else
            {
                print("PlayerState must be set to SPAWNING to spawn player. Current Player State is: " + sessionData.CurrentPlayerState);
            }
        }
        else
        {
            print("Spawn Point can't be found!");
        }

    }

    // Checks if there is already a player active, assigns the player object variable and ensures the player state is accurate. Might not be necessary
    public void CheckPlayerActive()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerObject = GameObject.FindGameObjectWithTag("Player");
            sessionData.UpdatePlayerState(SessionDataSO.PlayerState.ACTIVE);
        }
        else
        {
            playerObject = null;
            sessionData.UpdatePlayerState(SessionDataSO.PlayerState.INACTIVE);
        }

    }

    public void InitializeLevel()
    {
        sessionData.UpdateGameState(SessionDataSO.GameState.LEVELSTART);
        sessionData.UpdatePlayerState(SessionDataSO.PlayerState.INACTIVE);
        FindSpawnPoint();
        BuildTargetsList();
    }


}
