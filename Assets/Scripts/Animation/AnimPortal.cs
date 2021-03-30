using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Opens portal when player is spawning then closes it again once the player is spawned/active

public class AnimPortal : MonoBehaviour
{
    public SO_SessionData sessionData;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public float portalCloseDelay;

    private void Start()
    {
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
