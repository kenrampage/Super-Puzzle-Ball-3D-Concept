using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Acts as a second target for Cinemachine target group that is constantly on the ground directly below the spawn point or the player based on player state.

public class GroundCameraTarget : MonoBehaviour
{
    public SessionDataSO sessionData;

    private void FixedUpdate()
    {
        if (sessionData.CurrentPlayerState == SessionDataSO.PlayerState.ACTIVE)
        {
            
            transform.position = GameManager.Instance.playerObject.GetComponent<PlayerController3D>().groundPosition;
        }
        else
        {
            // transform.position = GameManager.Instance.spawnPoint.GetComponent<DistanceToGround>().groundPosition;
        }
    }

}
