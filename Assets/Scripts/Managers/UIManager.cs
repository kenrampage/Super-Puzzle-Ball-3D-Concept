using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public SessionDataSO sessionData;
    public WorldDatabaseSO worldDatabase;
    public GameObject gameUICanvas;
    // public GameObject uiOverlayCanvas;
    public GameObject pauseMenuCanvas;
    public GameObject levelStartCanvas;
    public GameObject levelEndCanvas;

    public bool debugMenuOn;


    // Start is called before the first frame update
    void Start()
    {
        sessionData.OnGameStateChanged.AddListener(HandleGameStateChanged);
        // GameManager.Instance.OnLevelChanged.AddListener(HandleLevelChanged);
        if(sessionData.CurrentGameState == SessionDataSO.GameState.LEVELSTART)
        {
            levelStartCanvas.gameObject.SetActive(true);
        }

    }

    // toggles various UI elements based on gamestate. Is there a better way to do this?
    void HandleGameStateChanged(SessionDataSO.GameState currentState, SessionDataSO.GameState previousState)
    {

        switch (currentState)
        {
            case SessionDataSO.GameState.PREGAME:
                gameUICanvas.gameObject.SetActive(false);
                pauseMenuCanvas.gameObject.SetActive(false);
                levelStartCanvas.gameObject.SetActive(false);
                levelEndCanvas.gameObject.SetActive(false);
                break;

            case SessionDataSO.GameState.RUNNING:

                // if (GameManager.Instance.debugMenuOn)
                // {
                //     mainMenuUICanvas.gameObject.SetActive(true);
                // }
                // else
                // {
                //     mainMenuUICanvas.gameObject.SetActive(false);
                // }
                gameUICanvas.gameObject.SetActive(true);
                pauseMenuCanvas.gameObject.SetActive(false);
                levelStartCanvas.gameObject.SetActive(false);
                levelEndCanvas.gameObject.SetActive(false);
                break;

            case SessionDataSO.GameState.PAUSED:
                gameUICanvas.gameObject.SetActive(true);
                pauseMenuCanvas.gameObject.SetActive(true);
                levelStartCanvas.gameObject.SetActive(false);
                levelEndCanvas.gameObject.SetActive(false);
                break;

            case SessionDataSO.GameState.LEVELSTART:
                gameUICanvas.gameObject.SetActive(false);
                pauseMenuCanvas.gameObject.SetActive(false);
                levelStartCanvas.gameObject.SetActive(true);
                levelEndCanvas.gameObject.SetActive(false);
                break;

            case SessionDataSO.GameState.LEVELEND:
                // if (GameManager.Instance.debugMenuOn)
                // {
                //     mainMenuUICanvas.gameObject.SetActive(true);
                // }
                // else
                // {
                //     mainMenuUICanvas.gameObject.SetActive(false);
                // }
                gameUICanvas.gameObject.SetActive(false);
                pauseMenuCanvas.gameObject.SetActive(false);
                levelEndCanvas.gameObject.SetActive(true);
                break;

            default:
                gameUICanvas.gameObject.SetActive(false);
                pauseMenuCanvas.gameObject.SetActive(false);
                levelStartCanvas.gameObject.SetActive(false);
                levelEndCanvas.gameObject.SetActive(false);
                break;
        }

    }

    // void HandleLevelChanged(string newLevel)
    // {
    //     gameUICanvas.transform.Find("CurrentLevelText").GetComponent<TextMeshProUGUI>().text = "Level: " + newLevel;
    // }

    public void TogglePauseMenu()
    {
        if (pauseMenuCanvas.activeSelf)
        {
            pauseMenuCanvas.gameObject.SetActive(false);
        }
        else 
        { 
            pauseMenuCanvas.gameObject.SetActive(true);
        }
    }

    void UpdateLevelText()
    {
        gameUICanvas.transform.Find("CurrentLevelText").GetComponent<TextMeshProUGUI>().text = worldDatabase.GetCurrentLevelName();
    }

}
