﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Opens portal when player is spawning then closes it again once the player is spawned/active

public class AnimPortal : MonoBehaviour
{
    public SessionDataSO sessionData;
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

    public void HandlePlayerStateChanged(SessionDataSO.PlayerState currentPlayerState, SessionDataSO.PlayerState previousPlayerState)
    {
        switch (currentPlayerState)
        {
            case SessionDataSO.PlayerState.INACTIVE:

                break;

            case SessionDataSO.PlayerState.SPAWNING:
                OpenPortal();
                
                break;

            case SessionDataSO.PlayerState.ACTIVE:
                if (previousPlayerState == SessionDataSO.PlayerState.SPAWNING)
                StartCoroutine(ClosePortal());
                break;

            case SessionDataSO.PlayerState.DESPAWNING:
               
                break;

            default:

                break;
        }
    }
}