using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AnimPortal : MonoBehaviour
{
    public SO_SessionData sessionData;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public float portalCloseDelay;

    private void Start()
    {
        sessionData.OnGameStateChanged.AddListener(HandleGameStateChanged);
        sessionData.OnPlayerStateChanged.AddListener(HandlePlayerStateChanged);
    }

    public void OpenPortal()
    {
        animator.SetBool("IsOpen", true);
    }

    public IEnumerator ClosePortal()
    {
        yield return new WaitForSecondsRealtime(portalCloseDelay);
        animator.SetBool("IsOpen", false);
    }

    public void HandleGameStateChanged(SO_SessionData.GameState currentGameState, SO_SessionData.GameState previousGameState)
    {
        switch (currentGameState)
        {
            case SO_SessionData.GameState.PREGAME:

                break;

            case SO_SessionData.GameState.RUNNING:

                break;

            case SO_SessionData.GameState.PAUSED:

                break;

            case SO_SessionData.GameState.LEVELSTART:
                
                break;

            case SO_SessionData.GameState.LEVELEND:

                break;

            default:

                break;
        }
    }

    public void HandlePlayerStateChanged(SO_SessionData.PlayerState currentPlayerState, SO_SessionData.PlayerState previousPlayerState)
    {
        switch (currentPlayerState)
        {
            case SO_SessionData.PlayerState.INACTIVE:

                break;

            case SO_SessionData.PlayerState.SPAWNING:
                OpenPortal();
                
                break;

            case SO_SessionData.PlayerState.ACTIVE:
                if (previousPlayerState == SO_SessionData.PlayerState.SPAWNING)
                StartCoroutine(ClosePortal());
                break;

            case SO_SessionData.PlayerState.DESPAWNING:
               
                break;

            default:

                break;
        }
    }
}
