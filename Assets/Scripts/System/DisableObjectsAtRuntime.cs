using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectsAtRuntime : MonoBehaviour
{

    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in objects)
        {
            item.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
