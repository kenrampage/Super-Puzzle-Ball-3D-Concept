using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimSpawnPipe : MonoBehaviour
{
    // Opens portal when player is spawning then closes it again once the player is spawned/active

    public SessionDataSO sessionData;
    public Animator animator;

    public float portalCloseDelay;

    [SerializeField] private UnityEvent onPipeOpen;
    [SerializeField] private UnityEvent onPipeClose;

    private void Start()
    {
        sessionData.OnPlayerStateChanged.AddListener(HandlePlayerStateChanged);
        sessionData.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    public void Open()
    {
        animator.SetBool("Open", true);
        onPipeOpen?.Invoke();
    }

    public IEnumerator Close()
    {
        yield return new WaitForSecondsRealtime(portalCloseDelay);
        animator.SetBool("Open", false);
        onPipeClose?.Invoke();
    }

    public void HandlePlayerStateChanged(SessionDataSO.PlayerState currentPlayerState, SessionDataSO.PlayerState previousPlayerState)
    {
        switch (currentPlayerState)
        {
            case SessionDataSO.PlayerState.INACTIVE:

                break;

            case SessionDataSO.PlayerState.SPAWNING:
                Open();

                break;

            case SessionDataSO.PlayerState.ACTIVE:

                break;

            case SessionDataSO.PlayerState.DESPAWNING:

                break;

            default:

                break;
        }
    }

    // toggles various UI elements based on gamestate. Is there a better way to do this?
    void HandleGameStateChanged(SessionDataSO.GameState currentState, SessionDataSO.GameState previousState)
    {

        switch (currentState)
        {
            case SessionDataSO.GameState.PREGAME:

                break;

            case SessionDataSO.GameState.RUNNING:
                if(previousState == SessionDataSO.GameState.LEVELSTART)
                {
                    StartCoroutine(Close());
                }
                break;

            case SessionDataSO.GameState.PAUSED:

                break;

            case SessionDataSO.GameState.LEVELSTART:

                break;

            case SessionDataSO.GameState.LEVELEND:

                break;

            default:

                break;
        }

    }
}
