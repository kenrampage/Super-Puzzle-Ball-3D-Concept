using UnityEngine;
using UnityEngine.Events;

public class HandleGameStateChanged : MonoBehaviour
{

    [SerializeField] private SessionDataSO sessionData;

    [SerializeField] private UnityEvent onPregame;
    [SerializeField] private UnityEvent onRunning;
    [SerializeField] private UnityEvent onPaused;
    [SerializeField] private UnityEvent onLevelStart;
    [SerializeField] private UnityEvent onLevelEnd;


    private void OnEnable()
    {
        sessionData.OnGameStateChanged.AddListener(HandleEvent);

    }

    private void OnDisable()
    {
        sessionData.OnGameStateChanged.RemoveListener(HandleEvent);
    }

    private void HandleEvent(SessionDataSO.GameState currentGameState, SessionDataSO.GameState previousGameState)
    {
        switch (currentGameState)
        {
            case SessionDataSO.GameState.PREGAME:
                onPregame?.Invoke();
                break;
            case SessionDataSO.GameState.RUNNING:
                onRunning?.Invoke();
                break;
            case SessionDataSO.GameState.PAUSED:
                onPaused?.Invoke();
                break;
            case SessionDataSO.GameState.LEVELSTART:
                onLevelStart?.Invoke();
                break;
            case SessionDataSO.GameState.LEVELEND:
                onLevelEnd?.Invoke();
                break;

            default:
                break;
        }
    }


}
