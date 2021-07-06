using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{

    public float targetWeight;
    public float targetRadius;
    public GameObject cameraTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CameraTargetTrigger")
        {
            GameManager.Instance.targetGroup.AddMember(cameraTarget.transform, targetWeight, targetRadius);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CameraTargetTrigger")
        {
            GameManager.Instance.targetGroup.RemoveMember(cameraTarget.transform);
        }

    }
}
