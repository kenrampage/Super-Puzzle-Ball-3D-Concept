using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredByPlayer : MonoBehaviour
{
    public Rigidbody playerRb;
    public bool triggerOn;


    private void OnTriggerEnter(Collider other)
    {
        playerRb = other.GetComponent<Rigidbody>();
        triggerOn = true;
    }

    private void OnTriggerExit(Collider other)
    {
                playerRb = null;
        triggerOn = false;
    }
}
