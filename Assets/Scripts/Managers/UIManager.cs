using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public GameObject gameUICanvas;
    public GameObject mainMenuUICanvas;
    public GameObject uiOverlayCanvas;

    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        GameManager.Instance.OnLevelChanged.AddListener(HandleLevelChanged);
        inputActions.UI.ToggleMenu.performed += _ => ToggleMainMenuCanvas();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        gameUICanvas.gameObject.SetActive(currentState == GameManager.GameState.LEVELSTART || currentState == GameManager.GameState.RUNNING);
    }

    void HandleLevelChanged(string currentLevel)
    {
        gameUICanvas.transform.Find("CurrentLevelText").GetComponent<TextMeshProUGUI>().text = "Level: " + currentLevel;
    }

    void ToggleMainMenuCanvas()
    {
        print("ToggleMenu Performed");
        mainMenuUICanvas.SetActive(!mainMenuUICanvas.activeSelf);
    }

}
