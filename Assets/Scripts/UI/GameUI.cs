using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameUI : MonoBehaviour
{
    public SO_SessionData sessionData;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI gameTimer;

    private string timerText;

    private void OnEnable()
    {
        currentLevelText.text = sessionData.currentLevelName;

    }

    private void Update()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(sessionData.levelTimer);
        timerText = timeSpan.ToString("mm':'ss'.'ff");
        gameTimer.text = timerText;
    }

}
