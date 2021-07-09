using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "WorldDatabase", menuName = "Scriptable Objects/Levels/World Database")]
public class WorldDatabaseSO : ScriptableObject
{
    public List<WorldSO> worlds = new List<WorldSO>();
    public int worldIndex = 0;

    // change current world
    public void SetWorldIndex(int index)
    {
        worldIndex = index;
    }

    // Reset world index
    public void ResetWorldIndex()
    {
        SetWorldIndex(0);
    }

    // get current worldSO
    public WorldSO GetCurrentWorld()
    {
        return worlds[worldIndex];
    }

    public string GetCurrentWorldDescription()
    {
        return worlds[worldIndex].description;
    }

    // Increment world index and get the next world SO
    public void StartNextWorld()
    {
        if (worldIndex < worlds.Count - 1)
        {
            worldIndex++;
            FirstLevel();
        }
        else
        {
            Debug.Log("At the end of the Worlds list!");
        }

    }

    // Load the main menu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Load the scene at the specified index
    public void LoadLevelWithIndex(int index)
    {
        if (index <= GetCurrentWorld().levels.Count - 1)
        {
            GetCurrentWorld().levelIndex = index;
            SceneManager.LoadScene(GetCurrentWorld().levels[index].sceneName);
        }
        else
        {
            MainMenu();
        }

    }

    // Load the first level
    public void FirstLevel()
    {
        LoadLevelWithIndex(0);
    }

    // Load next level
    public void NextLevel()
    {
        LoadLevelWithIndex(GetCurrentWorld().levelIndex + 1);
    }

    // restart current level
    public void RestartLevel()
    {
        LoadLevelWithIndex(GetCurrentWorld().levelIndex);
    }

    // Sets the level index
    public void SetLevelIndex(int index)
    {
        GetCurrentWorld().levelIndex = index;
    }

    //Resets level index for current world
    public void ResetLevelIndex()
    {
        GetCurrentWorld().levelIndex = 0;
    }

    // return current level name
    public string GetCurrentLevelName()
    {
        return GetCurrentWorld().levels[GetCurrentWorld().levelIndex].levelName;
    }

    // digs through all worlds and levels to update their indexes based on the loaded scene
    public void UpdateIndexes()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        foreach (WorldSO world in worlds)
        {
            foreach (LevelSO level in world.levels)
            {
                if(level.sceneName == currentScene)
                {
                    world.levelIndex = world.levels.IndexOf(level);
                    Debug.Log("Current Level Index is: " + world.levelIndex);

                    worldIndex = worlds.IndexOf(world);
                    Debug.Log("Current World Index is: " + worldIndex);
                }
            }
        }
    }

}

