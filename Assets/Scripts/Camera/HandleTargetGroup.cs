using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HandleTargetGroup : MonoBehaviour
{
    CinemachineTargetGroup targetGroup;

    private void Start()
    {

        GameManager.Instance.playerManager.OnPlayerIsActiveChanged.AddListener(UpdateTargetGroup);
        targetGroup = GetComponent<CinemachineTargetGroup>();

    }

    public void UpdateTargetGroup(GameObject playerObject)
    {
        if (GameManager.Instance.playerManager.playerIsActive)
        {
            targetGroup.RemoveMember(GameObject.Find("SpawnPoint").transform);
            targetGroup.AddMember(playerObject.transform, 1, 3.5f);
        }
    }

}
