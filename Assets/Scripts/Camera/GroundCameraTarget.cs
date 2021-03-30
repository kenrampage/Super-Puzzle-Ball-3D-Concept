using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCameraTarget : MonoBehaviour
{
    public SO_SessionData sessionData;

    private void FixedUpdate()
    {
        if (sessionData.CurrentPlayerState == SO_SessionData.PlayerState.ACTIVE)
        {
            
            transform.position = GameManager.Instance.playerObject.GetComponent<PlayerController>().groundPosition;
        }
        else
        {
            transform.position = GameManager.Instance.spawnPoint.GetComponent<DistanceToGround>().groundPosition;
        }
    }

}
