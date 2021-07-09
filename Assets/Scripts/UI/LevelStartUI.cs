using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelStartUI : MonoBehaviour
{
    public SessionDataSO sessionData;
    public WorldDatabaseSO worldDatabase;
    public TextMeshProUGUI currentLevelText;

    private void OnEnable()
    {
        UpdateCurrentLevelText();
    }

    private void UpdateCurrentLevelText()
    {
        currentLevelText.text = worldDatabase.GetCurrentLevelName();
    }

}
