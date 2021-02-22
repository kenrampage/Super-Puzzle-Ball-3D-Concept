using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState> { }
public class EventLevelData : UnityEvent<string> {}


public class GameManager : Singleton<GameManager>
{
    // keep track what level the game is currently in

    // keep track of game state
    // generate other persistent systems



    public GameObject[] systemPrefabs;
    private List<GameObject> instancedSystemPrefabs;

    public string currentLevelName = string.Empty;
    List<AsyncOperation> loadOperations;

    private int sceneCount;
    public List<string> scenesInBuild = new List<string>();

    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED,
        LEVELSTART,
        LEVELEND,
    }

    private GameState currentGameState;
    public GameState CurrentGameState
    {
        get { return currentGameState; }
        private set { currentGameState = value; }
    }

    public EventGameState OnGameStateChanged = new EventGameState();

    public GameObject scoreKeeper;
    public GameObject uiManager;

    public EventLevelData OnLevelChanged = new EventLevelData();



    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        instancedSystemPrefabs = new List<GameObject>();

        loadOperations = new List<AsyncOperation>();

        sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            scenesInBuild.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
            print(scenesInBuild[i].ToString());
        }

        //InstantiateSystemPrefabs();

        UpdateGameState(GameState.PREGAME);

    }

    void UpdateGameState(GameState state)
    {
        GameState previousGameState = currentGameState;
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
                Time.timeScale = 1f;
                break;
        }

        print("Current GameState: " + currentGameState);
        if (OnGameStateChanged != null)
        {
            OnGameStateChanged.Invoke(currentGameState, previousGameState);
        }

    }


    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
        }

        Debug.Log("Load Complete");
        UpdateGameState(GameState.LEVELSTART);
    }

    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete");
        UpdateGameState(GameState.PREGAME);
    }

    public void LoadLevel(string levelName)
    {
        if (currentLevelName == string.Empty)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
            if (ao == null)
            {
                Debug.Log("[GameManager] unable to load level " + levelName);
                return;
            }

            loadOperations.Add(ao);
            ao.completed += OnLoadOperationComplete;

            currentLevelName = levelName;
            OnLevelChanged.Invoke(currentLevelName);
        }
        else
        {
            Debug.Log(currentLevelName + " is already loaded. Unload the current scene before loading a new one");
        }

    }

    public void LoadLevelByIndex(int buildIndex)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.Log("[GameManager] unable to load level " + SceneManager.GetSceneByBuildIndex(buildIndex).name);
            return;
        }

        loadOperations.Add(ao);
        ao.completed += OnLoadOperationComplete;

        currentLevelName = SceneManager.GetSceneByBuildIndex(buildIndex).name;
        print(currentLevelName);
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
    }

    public void UnloadCurrentLevel()
    {
        if (currentLevelName != string.Empty)
        {
            UnloadLevel(currentLevelName);
            currentLevelName = string.Empty;

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

}
