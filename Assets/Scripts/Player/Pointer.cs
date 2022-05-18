using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public Camera gameCamera;
    private Vector3 aimDirection;

    private void Start()
    {
        gameCamera = Camera.main;
    }

    private void Update()

    {
        Vector3 mousePosition = gameCamera.ScreenToViewportPoint(Input.mousePosition);
        Vector3 pointerPosition = gameCamera.WorldToViewportPoint(transform.position);

        aimDirection = (new Vector3(mousePosition.x - pointerPosition.x, mousePosition.y - pointerPosition.y, 0)).normalized;
        // print(aimDirection);

        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        // print(aimAngle);

        transform.rotation = Quaternion.Euler(0,0,aimAngle);

        // Vector3 worldPoint = gameCamera.ScreenToWorldPoint(Input.mousePosition);
        // print(worldPoint);
        // this.transform.LookAt(worldPoint);

        
    }

    private void FixedUpdate()
    {

    }


}
