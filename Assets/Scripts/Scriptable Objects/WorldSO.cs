using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "World", menuName = "Scriptable Objects/Levels/World")]
public class WorldSO : ScriptableObject
{
    public string description;
    public int levelIndex = 0;
    public List<LevelSO> levels = new List<LevelSO>();

    // get current LevelSO
    public LevelSO GetCurrentLevel()
    {
        return levels[levelIndex];
    }



}
