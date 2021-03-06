using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelStartUI : MonoBehaviour
{
    public TextMeshProUGUI currentLevelText;

    private void OnEnable()
    {
        UpdateCurrentLevelText();
    }

    private void UpdateCurrentLevelText()
    {
        currentLevelText.text = GameManager.Instance.currentLevelName;
    }

}
