using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class PatrolObject : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3[] waypoints;
    private int waypointIndex;
    private bool isLoop;
    private bool objectIsMoving;

    public GameObject movingObject;
    public Vector3 offset;

    public float speed;
    public float waitTime;
    public bool moveForward;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        LineRendererSync();
        objectIsMoving = true;

        if (moveForward)
        {
            waypointIndex = 1;
        }
        else
        {
            waypointIndex = waypoints.Length - 1;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {

            LineRendererSync();
        }
        else
        {
            MoveObject();
        }


    }

    // Moves object towards the position of the waypoint based of the current waypointindex. Turns off movement once destination is reached
    void MoveObject()
    {

        if (movingObject.transform.position != waypoints[waypointIndex] && objectIsMoving)
        {
            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, waypoints[waypointIndex], speed * Time.deltaTime);
        }

        if (movingObject.transform.position == waypoints[waypointIndex] && objectIsMoving)
        {
            objectIsMoving = false;
            StartCoroutine(NextWayPoint());
        }

    }

    // calculates the next way point then waits the specified amount of time before turning movement back on
    IEnumerator NextWayPoint()
    {
        if (moveForward)
        {
            waypointIndex++;

            if (waypointIndex >= waypoints.Length)
            {
                if (isLoop)
                {
                    waypointIndex = 0;
                }
                else
                {
                    waypointIndex = waypoints.Length - 2;
                    moveForward = false;
                }

            }
        }
        else
        {
            waypointIndex--;
            if (waypointIndex < 0)
            {
                if (isLoop)
                {
                    waypointIndex = waypoints.Length - 1;
                }
                else
                {
                    waypointIndex = 1;
                    moveForward = true;
                }

            }
        }


        yield return new WaitForSeconds(waitTime);

        objectIsMoving = true;
    }


    // Editor function for syncing up the waypoint array and loop settings between the line renderer and this script
    void LineRendererSync()
    {
        waypoints = new Vector3[lineRenderer.positionCount];
        isLoop = lineRenderer.loop;
        lineRenderer.SetPosition(0, this.transform.position);

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            waypoints[i] = lineRenderer.GetPosition(i) - offset;
        }

        movingObject.transform.position = waypoints[0];

    }

}
