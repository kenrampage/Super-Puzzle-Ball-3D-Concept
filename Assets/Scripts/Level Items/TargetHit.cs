using UnityEngine;
using UnityEngine.Events;

public class TargetHit : MonoBehaviour
{
    [HideInInspector] public bool markerOn = true;
    public Animator animator;

    [SerializeField] private UnityEvent onTargetHit;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == GameManager.Instance.playerObject && markerOn)
        {
            GameManager.Instance.RemoveTarget(this.gameObject);
            animator.SetBool("Hit", true);
            onTargetHit?.Invoke();
            markerOn = false;
        }
    }
}
