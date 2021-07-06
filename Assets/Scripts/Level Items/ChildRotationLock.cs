using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChildRotationLock : MonoBehaviour
{

    public Transform parentObject;
    public bool continuousLock;


    private void Start()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, -parentObject.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (!Application.isPlaying || continuousLock)
        {
           this.transform.rotation = Quaternion.Euler(0, 0, -parentObject.rotation.z);
        }
        
    }
}
