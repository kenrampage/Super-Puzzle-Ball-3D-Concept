using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSet_", menuName = "Scriptable Objects/Levels/Level Set", order = 0)]
public class SO_LevelSet : ScriptableObject
{
    public string description;
    public List<string> levelList = new List<string>();

}

