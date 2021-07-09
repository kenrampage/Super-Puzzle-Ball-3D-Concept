using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelEndUI : MonoBehaviour
{
    public SessionDataSO sessionData;
    public WorldDatabaseSO worldDatabase;

    public TextMeshProUGUI levelEndText;
    public TextMeshProUGUI levelEndTimer;

    private string timerText;

    private void OnEnable()
    {
        UpdateLevelEndText();
    }

    private void UpdateLevelEndText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(sessionData.levelTimer);
        timerText = timeSpan.ToString("mm':'ss'.'ff");
        levelEndText.text = "End of " + worldDatabase.GetCurrentLevelName();
        levelEndTimer.text = "Your time: " + timerText;
    }

    public void LoadNextLevel()
    {
        // //current level set
        // SO_LevelSet levelSet = levelSetLibrary.setList[levelSetLibrary.currentLevelSetIndex];
        // //find current level in level set
        // int currentLevelIndex = levelSet.levelList.IndexOf(GameManager.Instance.currentLevelName);
        // //load the next level in the set
        // if(currentLevelIndex < levelSet.levelList.Count)
        // {
        //     GameManager.Instance.UnloadCurrentLevel();
        //     //print(levelSet.levelList[currentLevelIndex + 1]);
        //     GameManager.Instance.LoadLevel(levelSet.levelList[currentLevelIndex + 1]);
        // }

        //at the end of the set show the times for all the levels in the set
    }

}
