using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButton : MonoBehaviour
{
    public SO_SessionData sessionData;

    public void HandlePauseMenuButton()
    {

        switch (sessionData.CurrentGameState)
        {
            case SO_SessionData.GameState.PREGAME:
                GameManager.Instance.uiManager.ToggleMainMenuUI();
                break;

            case SO_SessionData.GameState.RUNNING:
                sessionData.UpdateGameState(SO_SessionData.GameState.PAUSED);
                break;

            case SO_SessionData.GameState.PAUSED:
                sessionData.UpdateGameState(SO_SessionData.GameState.RUNNING);
                break;

            case SO_SessionData.GameState.LEVELSTART:
                GameManager.Instance.uiManager.ToggleMainMenuUI();
                break;

            case SO_SessionData.GameState.LEVELEND:
                GameManager.Instance.uiManager.ToggleMainMenuUI();
                break;

            default:

                break;
        }


    }
}
