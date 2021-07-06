using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffScreenMarker : MonoBehaviour
{
    public Camera uiCamera;

    public GameObject targetObject;
    private Vector3 targetPosition;
    private Vector3 targetObjectScreenPoint;
    private float targetDistance;

    private TargetHit targetHitScript;

    public GameObject markerObject;
    public Image markerImage;
    private RectTransform markerRectTransform;
    private float markerAngle;
    private Vector3 markerPositionClamped;

    private bool isOffScreen;
    private bool isMarkerOn;

    public float borderSize = 100f;

    public float maxDistance = 50;


    private void Awake()
    {
        targetHitScript = targetObject.GetComponent<TargetHit>();
        targetPosition = targetObject.transform.position;
        markerRectTransform = markerObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        GetTargetStatus();

        if (isMarkerOn)
        {
            SetMarkerRotation();
            SetMarkerPosition();
            markerRectTransform.localEulerAngles = new Vector3(0, 0, markerAngle);
            targetObjectScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
            CheckOffScreen();
            SetMarkerPosition();
            SetMarkerStyle();
        }
        else
        {
            markerObject.SetActive(false);
        }


    }

    public void GetAngleFromVector(Vector3 dir)
    {
        markerAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (markerAngle < 0) markerAngle += 360;
    }

    private void CheckOffScreen()
    {

        isOffScreen = targetObjectScreenPoint.x <= 0 ||
            targetObjectScreenPoint.x >= Screen.width ||
            targetObjectScreenPoint.y <= 0 ||
            targetObjectScreenPoint.y >= Screen.height;

    }

    private void SetMarkerRotation()
    {
        Vector3 toPos = targetPosition;
        Vector3 fromPos = Camera.main.transform.position;

        fromPos.z = 0f;

        Vector3 dir = (toPos - fromPos).normalized;
        GetAngleFromVector(dir);
    }

    private void SetMarkerPosition()
    {
        if (isOffScreen)
        {
            markerObject.SetActive(true);
            markerPositionClamped = targetObjectScreenPoint;
            if (markerPositionClamped.x <= borderSize) markerPositionClamped.x = borderSize;
            if (markerPositionClamped.x >= Screen.width - borderSize) markerPositionClamped.x = Screen.width - borderSize;
            if (markerPositionClamped.y <= borderSize) markerPositionClamped.y = borderSize;
            if (markerPositionClamped.y >= Screen.height - borderSize) markerPositionClamped.y = Screen.height - borderSize;

            Vector3 markerWorldPosition = uiCamera.ScreenToWorldPoint(markerPositionClamped);
            markerRectTransform.position = new Vector3(markerWorldPosition.x, markerWorldPosition.y, 0);

        }
        else
        {
            markerObject.SetActive(false);
        }
    }

    private void SetMarkerStyle()
    {
        GetTargetDistance();
        // print("target distance: " + targetDistance);
        float newAlpha;


        if (targetDistance > maxDistance)
        {
            newAlpha = 0;
        }
        else
        {
            newAlpha = 1 - (targetDistance / maxDistance);
        }

        markerImage.color = new Color(markerImage.color.r, markerImage.color.g, markerImage.color.b, newAlpha);
        // print("newalpha: " + newAlpha);

    }

    private void GetTargetDistance()
    {
        Vector3 toPos = targetPosition;
        Vector3 fromPos = Camera.main.ScreenToWorldPoint(markerPositionClamped);
        fromPos.z = 0f;

        targetDistance = Vector3.Distance(fromPos, toPos);

    }

    private void GetTargetStatus()
    {
        isMarkerOn = targetHitScript.markerOn;
    }

}
