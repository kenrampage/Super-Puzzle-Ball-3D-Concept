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
                break;

            case GameManager.GameState.PAUSED:
                mainMenuUICanvas.gameObject.SetActive(true);
                gameUICanvas.gameObject.SetActive(true);
                pauseOverlayCanvas.gameObject.SetActive(true);
                break;

            case GameManager.GameState.LEVELSTART:
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
                gameUICanvas.gameObject.SetActive(true);
                pauseOverlayCanvas.gameObject.SetActive(false);
                break;

            default:
                mainMenuUICanvas.gameObject.SetActive(true);
                gameUICanvas.gameObject.SetActive(false);
                pauseOverlayCanvas.gameObject.SetActive(false);
                break;
        }

    }

    void HandleLevelChanged(string currentLevel)
    {
        gameUICanvas.transform.Find("CurrentLevelText").GetComponent<TextMeshProUGUI>().text = "Level: " + currentLevel;
    }

}
