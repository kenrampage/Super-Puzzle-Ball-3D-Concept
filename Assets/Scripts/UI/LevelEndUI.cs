using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEndUI : MonoBehaviour
{
    public TextMeshProUGUI levelEndText;
    public TextMeshProUGUI levelEndTimer;

    private void OnEnable()
    {
        UpdateLevelEndText();
    }

    private void UpdateLevelEndText()
    {
        levelEndText.text = "END OF " + GameManager.Instance.currentLevelName;
        levelEndTimer.text = "Your Time: " + GameManager.Instance.scoreKeeper.currentTimer.ToString();
    }

}
