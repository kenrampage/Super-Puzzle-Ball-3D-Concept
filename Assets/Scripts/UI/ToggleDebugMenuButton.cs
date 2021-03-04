using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleDebugMenuButton : MonoBehaviour
{

    public TextMeshProUGUI buttonText;

    private void Start()
    {
        buttonText.text = "Debug Menu Active: " + GameManager.Instance.debugMenuOn;
    }

    public void ToggleDebugMenu()
    {
        GameManager.Instance.debugMenuOn = !GameManager.Instance.debugMenuOn;
        print("Debug Menu Active: " + GameManager.Instance.debugMenuOn);

        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING && !GameManager.Instance.debugMenuOn)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.PAUSED);
        }

        buttonText.text = "Debug Menu Active: " + GameManager.Instance.debugMenuOn;
    }
}
