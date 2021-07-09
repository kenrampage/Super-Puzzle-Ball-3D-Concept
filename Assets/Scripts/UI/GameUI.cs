using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    public SessionDataSO sessionData;
    public WorldDatabaseSO worldDatabase;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI gameTimer;

    private string timerText;

    private void OnEnable()
    {
        currentLevelText.text = worldDatabase.GetCurrentLevelName();

    }

    private void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(sessionData.levelTimer);
        timerText = timeSpan.ToString("mm':'ss'.'ff");
        gameTimer.text = timerText;
    }

}
