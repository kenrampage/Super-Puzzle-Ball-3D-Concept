using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public SessionDataSO sessionData;
    List<AsyncOperation> loadOperations;

    private void Start()
    {
        loadOperations = new List<AsyncOperation>();
    }

    // runs when scene load operations are finished
    private void OnLoadOperationComplete(AsyncOperation ao)
    {

        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
        }

        UnloadMainMenu();

        //Debug.Log("Load Complete");
        // sessionData.UpdateGameState(SessionDataSO.GameState.LEVELSTART);
        
    }

    // runs when scene unload operations are finished
    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        //Debug.Log("Unload Complete");
        // sessionData.UpdateGameState(SessionDataSO.GameState.PREGAME);
    }

    // // Checks if currentLevelName variable is empty or matches the requested scene. If so it loads a scene by name then updates the currentLevelName
    // public void LoadLevel(string levelName)
    // {
    //     // CheckActiveScenes();

    //     if (sessionData.currentLevelName == string.Empty)
    //     {
    //         AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
    //         if (ao == null)
    //         {
    //             Debug.Log("[GameManager] unable to load level " + levelName);
    //             return;
    //         }

    //         loadOperations.Add(ao);
    //         ao.completed += OnLoadOperationComplete;

    //         sessionData.currentLevelName = levelName;
    //     }
    //     else if (sessionData.currentLevelName == levelName)
    //     {

    //         sessionData.UpdateGameState(SessionDataSO.GameState.LEVELSTART);
    //         print(levelName + " is already loaded. Changed GameState to LEVELSTART");
    //     }
    //     else
    //     {
    //         print(sessionData.currentLevelName + " already loaded. Unload the current level before trying to load a new one");
    //     }
    // }


    // // unloads specified scene and resets currentLevelName variable
    // public void UnloadLevel(string levelName)
    // {
    //     AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
    //     if (ao == null)
    //     {
    //         Debug.Log("[GameManager] unable to unload level " + levelName);
    //         return;
    //     }

    //     ao.completed += OnUnloadOperationComplete;

    //     sessionData.currentLevelName = string.Empty;
    // }

    // // unloads whatever the current level scene is
    // public void UnloadCurrentLevel()
    // {
    //     if (sessionData.currentLevelName != string.Empty)
    //     {
    //         UnloadLevel(sessionData.currentLevelName);
    //         sessionData.currentLevelName = string.Empty;
    //     }
    //     else
    //     {
    //         Debug.Log("No Level Currently Loaded");
    //     }
    // }

    public void UnloadMainMenu()
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync("MainMenu");
        if (ao == null)
        {
            Debug.Log("[GameManager] unable to MainMenu ");
            return;
        }

        ao.completed += OnUnloadOperationComplete;
    }
}
