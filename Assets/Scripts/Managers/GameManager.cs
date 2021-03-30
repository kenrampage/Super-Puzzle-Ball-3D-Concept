using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class EventLevelData : UnityEvent<string> { }


public class GameManager : Singleton<GameManager>
{
    public SO_SessionData sessionData;

    public GameObject[] systemPrefabs;
    private List<GameObject> instancedSystemPrefabs;

    List<AsyncOperation> loadOperations;

    public EventLevelData OnLevelChanged = new EventLevelData();

    public bool debugMenuOn;
    public UIManager uiManager;
    // public PlayerManager playerManager;

    private InputActions inputActions;

    public List<GameObject> targetsList = new List<GameObject>();
    public int targetCount;

    public GameObject playerPrefab;
    public float spawnDelay;


    [HideInInspector] public GameObject playerObject;
    [HideInInspector] public GameObject spawnPoint;

    override public void Awake()
    {
        base.Awake();
        inputActions = new InputActions();
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        // handles system prefabs
        //instancedSystemPrefabs = new List<GameObject>();
        //InstantiateSystemPrefabs();

        loadOperations = new List<AsyncOperation>();

        // signs up for events
        sessionData.OnGameStateChanged.AddListener(HandleGameStateChanged);
        sessionData.OnPlayerStateChanged.AddListener(HandlePlayerStateChanged);
        inputActions.UI.ToggleMenu.performed += _ => ToggleGamePaused();

        // reset state machines
        sessionData.UpdateGameState(SO_SessionData.GameState.PREGAME);
        sessionData.UpdatePlayerState(SO_SessionData.PlayerState.INACTIVE);

        // checks if there is already a game scene loaded
        CheckActiveScenes();

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

    // toggles paused/running gamestate
    private void ToggleGamePaused()
    {
        if (sessionData.CurrentGameState == SO_SessionData.GameState.RUNNING)
        {
            sessionData.UpdateGameState(SO_SessionData.GameState.PAUSED);
        }
        else if (sessionData.CurrentGameState == SO_SessionData.GameState.PAUSED)
        {
            sessionData.UpdateGameState(SO_SessionData.GameState.RUNNING);
        }

    }

    // runs when scene load operations are finished
    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
        }

        //Debug.Log("Load Complete");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sessionData.currentLevelName));
        sessionData.UpdateGameState(SO_SessionData.GameState.LEVELSTART);
    }

    // runs when scene unload operations are finished
    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        //Debug.Log("Unload Complete");
        sessionData.UpdateGameState(SO_SessionData.GameState.PREGAME);
    }

    // Checks if currentLevelName variable is empty or matches the requested scene. If so it loads a scene by name then updates the currentLevelName
    public void LoadLevel(string levelName)
    {
        CheckActiveScenes();

        if (sessionData.currentLevelName == string.Empty)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            if (ao == null)
            {
                Debug.Log("[GameManager] unable to load level " + levelName);
                return;
            }

            loadOperations.Add(ao);
            ao.completed += OnLoadOperationComplete;

            sessionData.currentLevelName = levelName;
        }
        else if (sessionData.currentLevelName == levelName)
        {

            sessionData.UpdateGameState(SO_SessionData.GameState.LEVELSTART);
            print(levelName + " is already loaded. Changed GameState to LEVELSTART");
        }
        else
        {
            print(sessionData.currentLevelName + " already loaded. Unload the current level before trying to load a new one");
        }
    }


    // unloads specified scene and resets currentLevelName variable
    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.Log("[GameManager] unable to unload level " + levelName);
            return;
        }

        ao.completed += OnUnloadOperationComplete;

        sessionData.currentLevelName = string.Empty;
    }

    // unloads whatever the current level scene is
    public void UnloadCurrentLevel()
    {
        CheckActiveScenes();

        if (sessionData.currentLevelName != string.Empty)
        {
            UnloadLevel(sessionData.currentLevelName);
            sessionData.currentLevelName = string.Empty;
        }
        else
        {
            Debug.Log("No Level Currently Loaded");
        }
    }

    // instantiates system prefabs from the serialized list
    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;

        for (int i = 0; i < systemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(systemPrefabs[i]);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

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

    // checks which scenes are currently loaded. If there is a level loaded it assigns it to the currentLevelName variable and sets it as active
    private void CheckActiveScenes()
    {
        int activeSceneCount = SceneManager.sceneCount;

        for (int i = 0; i < activeSceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name != "MainMenu")
            {
                sessionData.currentLevelName = SceneManager.GetSceneAt(i).name;
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sessionData.currentLevelName));
                break;
            }
            else
            {
                sessionData.currentLevelName = string.Empty;
            }
        }
    }


    // might not be necessary anymore
    // public void InitializeLevel()
    // {
    //     if (sessionData.CurrentGameState == SO_SessionData.GameState.PREGAME && sessionData.currentLevelName != null)
    //     {
    //         sessionData.UpdateGameState(SO_SessionData.GameState.LEVELSTART);
    //     }
    //     else
    //     {
    //         print("Level Already Initialized!");
    //     }

    // }

    // function to state the level from a UI button
    public void StartLevel()
    {
        if (sessionData.currentLevelName != null && sessionData.CurrentGameState == SO_SessionData.GameState.LEVELSTART)
        {
            sessionData.UpdateGameState(SO_SessionData.GameState.RUNNING);
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
            sessionData.UpdateGameState(SO_SessionData.GameState.LEVELEND);
        }
    }

    // Statemachine for GameState
    public void HandleGameStateChanged(SO_SessionData.GameState currentGameState, SO_SessionData.GameState previousGameState)
    {
        switch (currentGameState)
        {
            case SO_SessionData.GameState.PREGAME:

                break;

            case SO_SessionData.GameState.RUNNING:
                // Spawns player if the game level is running initially
                if (previousGameState == SO_SessionData.GameState.LEVELSTART)
                    sessionData.UpdatePlayerState(SO_SessionData.PlayerState.SPAWNING);
                break;

            case SO_SessionData.GameState.PAUSED:

                break;

            case SO_SessionData.GameState.LEVELSTART:

                sessionData.ResetTimer();
                FindSpawnPoint();
                BuildTargetsList();
                break;

            case SO_SessionData.GameState.LEVELEND:
                sessionData.UpdatePlayerState(SO_SessionData.PlayerState.DESPAWNING);
                break;

            default:

                break;
        }

    }

    public void HandlePlayerStateChanged(SO_SessionData.PlayerState currentPlayerState, SO_SessionData.PlayerState previousPlayerState)
    {
        switch (currentPlayerState)
        {
            case SO_SessionData.PlayerState.INACTIVE:
                Destroy(playerObject);
                break;

            case SO_SessionData.PlayerState.SPAWNING:
                StartCoroutine(SpawnPlayer());
                break;

            case SO_SessionData.PlayerState.ACTIVE:
                break;

            case SO_SessionData.PlayerState.DESPAWNING:

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
            if (sessionData.CurrentPlayerState == SO_SessionData.PlayerState.SPAWNING)
            {

                playerObject = Instantiate(playerPrefab, spawnPoint.transform.position, playerPrefab.transform.rotation);
                sessionData.UpdatePlayerState(SO_SessionData.PlayerState.ACTIVE);

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
            sessionData.UpdatePlayerState(SO_SessionData.PlayerState.ACTIVE);
        }
        else
        {
            playerObject = null;
            sessionData.UpdatePlayerState(SO_SessionData.PlayerState.INACTIVE);
        }

    }


}
