using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnCollisionEvent : UnityEvent<float>
{

}

public class GetCollisionVelocity : MonoBehaviour
{
    private Vector3 collisionAngle;
    private Vector2 correctedCollisionAngle;
    private float collisionVelocity;

    private Rigidbody rb;

    public OnCollisionEvent onCollisionEvent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        collisionAngle = transform.position - other.GetContact(0).point;
        CalcCollisionVelocity();
        onCollisionEvent.Invoke(collisionVelocity);
    }

    private void CalcCollisionVelocity()
    {
        correctedCollisionAngle.x = Mathf.Abs(collisionAngle.x);
        correctedCollisionAngle.y = Mathf.Abs(collisionAngle.y);

        if (correctedCollisionAngle.x > correctedCollisionAngle.y)
        {
            collisionVelocity = (Mathf.Abs(rb.velocity.x) * correctedCollisionAngle.x);

        }
        else if (correctedCollisionAngle.x < correctedCollisionAngle.y)
        {
            collisionVelocity = (Mathf.Abs(rb.velocity.y) * correctedCollisionAngle.y);
        }
        else
        {
            collisionVelocity = ((Mathf.Abs(rb.velocity.x) * correctedCollisionAngle.x) + (Mathf.Abs(rb.velocity.y) * correctedCollisionAngle.y)) / 2 ;
        }

    }

}
