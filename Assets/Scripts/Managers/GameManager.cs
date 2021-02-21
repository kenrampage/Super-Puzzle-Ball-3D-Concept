using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // keep track what level the game is currently in

    // keep track of game state
    // generate other persistent systems


    public GameObject[] systemPrefabs;
    private List<GameObject> instancedSystemPrefabs;

    private string currentLevelName = string.Empty;
    List<AsyncOperation> loadOperations;

    private int sceneCount;
    public List<string> scenesInBuild = new List<string>();


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

        InstantiateSystemPrefabs();

        //LoadLevel("Level01");
    }


    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
        }

        Debug.Log("Load Complete");
    }

    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete");
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
