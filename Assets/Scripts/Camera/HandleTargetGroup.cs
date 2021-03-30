using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HandleTargetGroup : MonoBehaviour
{
    public SO_SessionData sessionData;
    CinemachineTargetGroup targetGroup;

    private void Start()
    {

        sessionData.OnPlayerStateChanged.AddListener(HandlePlayerStateChanged);
        targetGroup = GetComponent<CinemachineTargetGroup>();

    }

    public void UpdateTargetGroup(GameObject playerObject)
    {
        GameObject spawnPoint = GameManager.Instance.spawnPoint;
        float spawnPointWeight = targetGroup.m_Targets[targetGroup.FindMember(spawnPoint.transform)].weight;
        float spawnPointRadius = targetGroup.m_Targets[targetGroup.FindMember(spawnPoint.transform)].radius;     
           
        targetGroup.RemoveMember(GameManager.Instance.spawnPoint.transform);
        targetGroup.AddMember(playerObject.transform, spawnPointWeight, spawnPointRadius);

    }

    public void HandlePlayerStateChanged(SO_SessionData.PlayerState currentPlayerState, SO_SessionData.PlayerState previousPlayerState)
    {
        switch (currentPlayerState)
        {
            case SO_SessionData.PlayerState.INACTIVE:

                break;

            case SO_SessionData.PlayerState.SPAWNING:
                

                break;

            case SO_SessionData.PlayerState.ACTIVE:
                UpdateTargetGroup(GameManager.Instance.playerObject);
                break;

            case SO_SessionData.PlayerState.DESPAWNING:

                break;

            default:

                break;
        }
    }

}
