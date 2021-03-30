using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SO_LevelSetLibrary", menuName = "Scriptable Objects/Levels/Level Set Library", order = 0)]
public class SO_LevelSetLibrary : ScriptableObject
{
    public List<SO_LevelSet> setList = new List<SO_LevelSet>();
}
