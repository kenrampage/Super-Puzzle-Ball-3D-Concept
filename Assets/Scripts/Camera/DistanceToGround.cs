using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Calculates the distance to the closest object below this one that's in the ground layer mask.

public class DistanceToGround : MonoBehaviour
{
    public Vector2 groundPosition;
    public LayerMask groundLayerMask;

    private void Start()
    {
        GetDistanceToGround();
    }

    public void GetDistanceToGround()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, groundLayerMask);

        if (raycastHit.collider != null)
        {
            groundPosition = new Vector2(transform.position.x, transform.position.y - raycastHit.distance);
        }
        Color rayColor = Color.red;
        Debug.DrawRay(transform.position, Vector2.down * raycastHit.distance, rayColor);
    }
}
