using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Level", menuName = "Scriptable Objects/Levels/Level")]
public class LevelSO : ScriptableObject
{

    public string levelName;
    public string description;

    public int difficulty;

    public float bestTime;
    public float bronzeTime;
    public float silverTime;
    public float goldTime;

    public int newRank;
    public int currentRank;
    public int previousRank;

    public string sceneName;
    public Image thumbnail;



}
