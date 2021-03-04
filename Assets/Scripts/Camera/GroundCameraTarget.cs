using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCameraTarget : MonoBehaviour
{

    private void FixedUpdate()
    {
        if (GameManager.Instance.playerManager.playerIsActive)
        {
            
            transform.position = GameManager.Instance.playerManager.playerObject.GetComponent<PlayerController>().groundPosition;
        }
        else
        {
            transform.position = GameObject.Find("SpawnPoint").GetComponent<DistanceToGround>().groundPosition;
        }
    }

}
