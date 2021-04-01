using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButton : MonoBehaviour
{
    public SessionDataSO sessionData;

    public void HandlePauseMenuButton()
    {

        switch (sessionData.CurrentGameState)
        {
            case SessionDataSO.GameState.PREGAME:
                // GameManager.Instance.uiManager.ToggleMainMenuUI();
                break;

            case SessionDataSO.GameState.RUNNING:
                sessionData.UpdateGameState(SessionDataSO.GameState.PAUSED);
                break;

            case SessionDataSO.GameState.PAUSED:
                sessionData.UpdateGameState(SessionDataSO.GameState.RUNNING);
                break;

            case SessionDataSO.GameState.LEVELSTART:
                // GameManager.Instance.uiManager.ToggleMainMenuUI();
                break;

            case SessionDataSO.GameState.LEVELEND:
                // GameManager.Instance.uiManager.ToggleMainMenuUI();
                break;

            default:

                break;
        }


    }
}
