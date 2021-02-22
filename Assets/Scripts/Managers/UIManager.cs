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

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        GameManager.Instance.OnLevelChanged.AddListener(HandleLevelChanged);
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        gameUICanvas.gameObject.SetActive(currentState == GameManager.GameState.LEVELSTART || currentState == GameManager.GameState.RUNNING);
    }

    void HandleLevelChanged(string currentLevel)
    {
        gameUICanvas.transform.Find("CurrentLevelText").GetComponent<TextMeshProUGUI>().text = "Level: " + currentLevel;
    }

}
