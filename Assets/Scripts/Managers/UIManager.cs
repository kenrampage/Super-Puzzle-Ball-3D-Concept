using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public GameObject gameUICanvas;
    public GameObject mainMenuUICanvas;
    public GameObject uiOverlayCanvas;
    public GameObject pauseOverlayCanvas;
    public GameObject levelStartCanvas;
    public GameObject levelEndCanvas;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        GameManager.Instance.OnLevelChanged.AddListener(HandleLevelChanged);

    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {

        switch (currentState)
        {
            case GameManager.GameState.PREGAME:
                mainMenuUICanvas.gameObject.SetActive(true);
                gameUICanvas.gameObject.SetActive(false);
                pauseOverlayCanvas.gameObject.SetActive(false);
                levelStartCanvas.gameObject.SetActive(false);
                levelEndCanvas.gameObject.SetActive(false);
                break;

            case GameManager.GameState.RUNNING:

                if (GameManager.Instance.debugMenuOn)
                {
                    mainMenuUICanvas.gameObject.SetActive(true);
                }
                else
                {
                    mainMenuUICanvas.gameObject.SetActive(false);
                }
                gameUICanvas.gameObject.SetActive(true);
                pauseOverlayCanvas.gameObject.SetActive(false);
                levelStartCanvas.gameObject.SetActive(false);
                levelEndCanvas.gameObject.SetActive(false);
                break;

            case GameManager.GameState.PAUSED:
                mainMenuUICanvas.gameObject.SetActive(true);
                gameUICanvas.gameObject.SetActive(true);
                pauseOverlayCanvas.gameObject.SetActive(true);
                levelStartCanvas.gameObject.SetActive(false);
                levelEndCanvas.gameObject.SetActive(false);
                break;

            case GameManager.GameState.LEVELSTART:
                mainMenuUICanvas.gameObject.SetActive(false);
                gameUICanvas.gameObject.SetActive(false);
                pauseOverlayCanvas.gameObject.SetActive(false);
                levelStartCanvas.gameObject.SetActive(true);
                levelEndCanvas.gameObject.SetActive(false);
                break;

            case GameManager.GameState.LEVELEND:
                if (GameManager.Instance.debugMenuOn)
                {
                    mainMenuUICanvas.gameObject.SetActive(true);
                }
                else
                {
                    mainMenuUICanvas.gameObject.SetActive(false);
                }
                gameUICanvas.gameObject.SetActive(false);
                pauseOverlayCanvas.gameObject.SetActive(false);
                levelEndCanvas.gameObject.SetActive(true);
                break;

            default:
                mainMenuUICanvas.gameObject.SetActive(true);
                gameUICanvas.gameObject.SetActive(false);
                pauseOverlayCanvas.gameObject.SetActive(false);
                levelStartCanvas.gameObject.SetActive(false);
                levelEndCanvas.gameObject.SetActive(false);
                break;
        }

    }

    void HandleLevelChanged(string newLevel)
    {
        gameUICanvas.transform.Find("CurrentLevelText").GetComponent<TextMeshProUGUI>().text = "Level: " + newLevel;
    }

    public void ToggleMainMenuUI()
    {
        if (mainMenuUICanvas.activeSelf)
        {
            mainMenuUICanvas.gameObject.SetActive(false);
        }
        else 
        { 
            mainMenuUICanvas.gameObject.SetActive(true);
        }
    }

}
