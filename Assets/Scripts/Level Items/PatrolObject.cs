using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class PatrolObject : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3[] waypointPositions;
    private int waypointIndex;
    private bool isLoop;
    private bool objectIsMoving;

    public GameObject movingObject;
    public GameObject rumbleObject;
    public ProgressBar timer;

    public float speed;
    public float waitTime;
    public bool moveForward;

    public bool rumbleOn;
    public float rumbleAmount = .05f;

    [SerializeField] private UnityEvent onMoveStart;
    [SerializeField] private UnityEvent onMoveStop;


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
            waypointIndex = waypointPositions.Length - 1;
        }

        timer.minValue = 0;
        timer.maxValue = waitTime;

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

        Timer();

    }

    // Moves object towards the position of the waypoint based of the current waypointindex. Turns off movement once destination is reached
    void MoveObject()
    {

        if (movingObject.transform.position != waypointPositions[waypointIndex] && objectIsMoving)
        {
            movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, waypointPositions[waypointIndex], speed * Time.deltaTime);
            if (rumbleOn)
            {
                ChildObjectRumble();
            }

        }

        if (movingObject.transform.position == waypointPositions[waypointIndex] && objectIsMoving)
        {
            // objectIsMoving = false;
            StopMoving();
            StartCoroutine(NextWayPoint());
        }




    }

    // calculates the next way point then waits the specified amount of time before turning movement back on
    IEnumerator NextWayPoint()
    {
        if (moveForward)
        {
            waypointIndex++;

            if (waypointIndex >= waypointPositions.Length)
            {
                if (isLoop)
                {
                    waypointIndex = 0;
                }
                else
                {
                    waypointIndex = waypointPositions.Length - 2;
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
                    waypointIndex = waypointPositions.Length - 1;
                }
                else
                {
                    waypointIndex = 1;
                    moveForward = true;
                }

            }
        }


        yield return new WaitForSeconds(waitTime);

        // objectIsMoving = true;
        StartMoving();
    }


    // Editor function for syncing up the waypoint array and loop settings between the line renderer and this script
    void LineRendererSync()
    {
        waypointPositions = new Vector3[lineRenderer.positionCount];
        isLoop = lineRenderer.loop;
        lineRenderer.SetPosition(0, this.transform.position);

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            waypointPositions[i] = lineRenderer.GetPosition(i);
        }

        movingObject.transform.position = waypointPositions[0];

    }

    private void Timer()
    {
        if (!objectIsMoving)
        {
            timer.currentValue += Time.deltaTime;
        }
        else
        {
            if (timer.currentValue > 0)
            {
                timer.currentValue -= Time.deltaTime * 5;
            }

        }

    }

    private void ChildObjectRumble()
    {
        rumbleObject.transform.localPosition = new Vector3(Random.Range(-rumbleAmount, rumbleAmount), Random.Range(-rumbleAmount, rumbleAmount), rumbleObject.transform.localPosition.z);
    }

    private void StartMoving()
    {
        objectIsMoving = true;
        onMoveStart?.Invoke();
    }

    private void StopMoving()
    {
        objectIsMoving = false;
        onMoveStop?.Invoke();
    }

}
