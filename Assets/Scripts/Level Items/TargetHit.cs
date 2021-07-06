using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour
{
    [HideInInspector] public bool markerOn = true;
    public Animator animator;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.Instance.playerObject)
        {
            GameManager.Instance.RemoveTarget(this.gameObject);
            animator.SetBool("Hit", true);
            markerOn = false;
        }
    }
}
