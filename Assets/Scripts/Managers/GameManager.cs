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
        instancedSystemPrefabs = new List<GameObject>();
        loadOperations = new List<AsyncOperation>();
        //InstantiateSystemPrefabs();
        sessionData.UpdateGameState(SO_SessionData.GameState.PREGAME);
        sessionData.UpdatePlayerState(SO_SessionData.PlayerState.INACTIVE);
        CheckActiveScenes();

        sessionData.OnGameStateChanged.AddListener(HandleGameStateChanged);
        sessionData.OnPlayerStateChanged.AddListener(HandlePlayerStateChanged);
        inputActions.UI.ToggleMenu.performed += _ => ToggleGamePaused();
        sessionData.ResetTimer();
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

    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        //Debug.Log("Unload Complete");
        sessionData.UpdateGameState(SO_SessionData.GameState.PREGAME);
    }

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

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;

        for (int i = 0; i < systemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(systemPrefabs[i]);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < instancedSystemPrefabs.Count; i++)
        {
            Destroy(instancedSystemPrefabs[i]);
        }

        instancedSystemPrefabs.Clear();

    }

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


    public void InitializeLevel()
    {
        if (sessionData.CurrentGameState == SO_SessionData.GameState.PREGAME && sessionData.currentLevelName != null)
        {
            sessionData.UpdateGameState(SO_SessionData.GameState.LEVELSTART);
        }
        else
        {
            print("Level Already Initialized!");
        }

    }

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

    public void ResetTargetsList()
    {
        targetsList.Clear();
        targetCount = 0;
    }

    public void RemoveTarget(GameObject target)
    {
        targetsList.Remove(target);
        CheckTargetsRemaining();
    }

    public void CheckTargetsRemaining()
    {
        targetCount = targetsList.Count;
        print(targetCount + " targets remaining");
        if (targetCount == 0)
        {
            sessionData.UpdateGameState(SO_SessionData.GameState.LEVELEND);
        }
    }

    public void HandleGameStateChanged(SO_SessionData.GameState currentGameState, SO_SessionData.GameState previousGameState)
    {
        switch (currentGameState)
        {
            case SO_SessionData.GameState.PREGAME:

                break;

            case SO_SessionData.GameState.RUNNING:
                if (previousGameState == SO_SessionData.GameState.LEVELSTART)
                    sessionData.UpdatePlayerState(SO_SessionData.PlayerState.SPAWNING);
                break;

            case SO_SessionData.GameState.PAUSED:

                break;

            case SO_SessionData.GameState.LEVELSTART:
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

    public void FindSpawnPoint()
    {
        spawnPoint = GameObject.Find("SpawnPoint");
    }

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
