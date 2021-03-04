using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuButton : MonoBehaviour
{
    public void HandlePauseMenuButton()
    {

        switch (GameManager.Instance.CurrentGameState)
        {
            case GameManager.GameState.PREGAME:

                break;

            case GameManager.GameState.RUNNING:
                GameManager.Instance.UpdateGameState(GameManager.GameState.PAUSED);
                break;

            case GameManager.GameState.PAUSED:
                GameManager.Instance.UpdateGameState(GameManager.GameState.RUNNING);
                break;

            case GameManager.GameState.LEVELSTART:

                break;

            case GameManager.GameState.LEVELEND:

                break;

            default:

                break;
        }


    }
}
