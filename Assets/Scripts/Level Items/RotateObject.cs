using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject objectToRotate;
    public float waitTime;
    private float rotTimeA;

    public float rotDegreesA;
    public float rotTimeAB;
    public float rotDegreesB;
    public float rotTimeBA;

    Vector3 rotA;
    Vector3 rotAB;
    Vector3 rotBA;

    private bool firstRot;


    void Start()
    {
        firstRot = true;

        rotA = transform.eulerAngles + new Vector3(0f, 0f, rotDegreesA);

        rotAB = transform.eulerAngles + new Vector3(0f, 0f, rotDegreesB - rotDegreesA);

        rotBA = transform.eulerAngles + new Vector3(0f, 0f, rotDegreesA - rotDegreesB);

        
        rotTimeA = rotTimeBA * (Mathf.Abs(rotDegreesA) / Mathf.Abs(rotDegreesB - rotDegreesA));
        print(rotTimeA);

        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            if (firstRot)
            {
                yield return RotateTargetObject(objectToRotate, rotA, rotTimeA);
            }
            firstRot = false;

            yield return new WaitForSeconds(waitTime);

            yield return RotateTargetObject(objectToRotate, rotAB, rotTimeAB);
            yield return new WaitForSeconds(waitTime);

            yield return RotateTargetObject(objectToRotate, rotBA, rotTimeBA);
            yield return new WaitForSeconds(waitTime);

        }
    }

    bool rotating = false;
    IEnumerator RotateTargetObject(GameObject gameObjectToMove, Vector3 eulerAngles, float duration)
    {
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        Vector3 newRot = gameObjectToMove.transform.eulerAngles + eulerAngles;

        Vector3 currentRot = gameObjectToMove.transform.eulerAngles;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.eulerAngles = Vector3.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        
        rotating = false;

    }
}
