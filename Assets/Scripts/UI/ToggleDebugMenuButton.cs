using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleDebugMenuButton : MonoBehaviour
{
    public SessionDataSO sessionData;
    public TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText.text = "Debug Menu Active: " + GameManager.Instance.uiManager.debugMenuOn;
    }

    public void ToggleDebugMenu()
    {
        GameManager.Instance.uiManager.debugMenuOn = !GameManager.Instance.uiManager.debugMenuOn;
        print("Debug Menu Active: " + GameManager.Instance.uiManager.debugMenuOn);

        if (sessionData.CurrentGameState == SessionDataSO.GameState.RUNNING && !GameManager.Instance.uiManager.debugMenuOn)
        {
            sessionData.UpdateGameState(SessionDataSO.GameState.PAUSED);
        }

        buttonText.text = "Debug Menu Active: " + GameManager.Instance.uiManager.debugMenuOn;
    }
}
