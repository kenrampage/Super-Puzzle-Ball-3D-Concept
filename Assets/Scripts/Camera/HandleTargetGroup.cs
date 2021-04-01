using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HandleTargetGroup : MonoBehaviour
{
    public SessionDataSO sessionData;
    public float targetWeight;
    public float targetRadius;
    CinemachineTargetGroup targetGroup;

    private void Start()
    {

        sessionData.OnPlayerStateChanged.AddListener(HandlePlayerStateChanged);
        targetGroup = GetComponent<CinemachineTargetGroup>();

    }

    // Removes the spawnpoint from the cinemachine target group and adds the player object
    public void UpdateTargetGroup()
    {
        GameObject spawnPoint = GameManager.Instance.spawnPoint;
        GameObject playerObject = GameManager.Instance.playerObject;

        targetGroup.RemoveMember(spawnPoint.transform);
        targetGroup.AddMember(playerObject.transform, targetWeight, targetRadius);

    }

    // Assigns the Cinemachine targetgroup primary target based on player state
    public void HandlePlayerStateChanged(SessionDataSO.PlayerState currentPlayerState, SessionDataSO.PlayerState previousPlayerState)
    {
        switch (currentPlayerState)
        {
            case SessionDataSO.PlayerState.INACTIVE:
                
                break;

            case SessionDataSO.PlayerState.SPAWNING:


                break;

            case SessionDataSO.PlayerState.ACTIVE:
                UpdateTargetGroup();
                break;

            case SessionDataSO.PlayerState.DESPAWNING:

                break;

            default:

                break;
        }
    }

}
