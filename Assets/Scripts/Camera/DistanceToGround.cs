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

    // checks the distance to the object directly below the player on the ground layer mask. Used for positioning the ground target for cinemachine
    public void GetDistanceToGround()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, Mathf.Infinity, groundLayerMask))

            groundPosition = new Vector3(transform.position.x, transform.position.y - raycastHit.distance, transform.position.z);

        Color rayColor = Color.red;
        Debug.DrawRay(transform.position, Vector2.down * raycastHit.distance, rayColor);
    }
}
