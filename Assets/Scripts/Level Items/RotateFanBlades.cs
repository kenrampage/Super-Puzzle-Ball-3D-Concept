using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFanBlades : MonoBehaviour
{
    [SerializeField] private GameObject fanBlades;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private bool rotateReverse;

    private void Start()
    {
        if(rotateReverse)
        {
            fanBlades.transform.localScale = new Vector3(-fanBlades.transform.localScale.x,fanBlades.transform.localScale.y,fanBlades.transform.localScale.z);
            rotateSpeed = -rotateSpeed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        fanBlades.transform.Rotate(0, 0, rotateSpeed);
    }
}
