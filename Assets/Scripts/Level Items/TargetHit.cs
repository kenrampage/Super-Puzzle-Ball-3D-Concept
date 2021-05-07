using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHit : MonoBehaviour
{

    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.Instance.playerObject)
        {
            GameManager.Instance.RemoveTarget(this.gameObject);
            animator.SetBool("Hit", true);
        }
    }
}
