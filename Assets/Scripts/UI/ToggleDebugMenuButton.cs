using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleDebugMenuButton : MonoBehaviour
{
    public SO_SessionData sessionData;
    public TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText.text = "Debug Menu Active: " + GameManager.Instance.debugMenuOn;
    }

    public void ToggleDebugMenu()
    {
        GameManager.Instance.debugMenuOn = !GameManager.Instance.debugMenuOn;
        print("Debug Menu Active: " + GameManager.Instance.debugMenuOn);

        if (sessionData.CurrentGameState == SO_SessionData.GameState.RUNNING && !GameManager.Instance.debugMenuOn)
        {
            sessionData.UpdateGameState(SO_SessionData.GameState.PAUSED);
        }

        buttonText.text = "Debug Menu Active: " + GameManager.Instance.debugMenuOn;
    }
}
