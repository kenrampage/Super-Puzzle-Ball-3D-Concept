using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WindTrap : MonoBehaviour
{
    private bool windOn;
    private Vector3 blowerPos;

    public ProgressBar timer;
    public TriggeredByPlayer windArea;
    public GameObject blower;
    public ParticleSystem particles;

    public float windStrength;
    public Vector3 windDirection;
    public float blowTime;
    public float cooldownTime;

    public float rumbleAmount;

    [SerializeField] private UnityEvent onWindStart;
    [SerializeField] private UnityEvent onWindStop;

    private void Start()
    {
        blowerPos = blower.transform.position;
        windOn = false;
    }


    private void Update()
    {
        Timer();
    }

    private void FixedUpdate()
    {
        if(windOn)
        {
            blower.transform.position = blowerPos + new Vector3(Random.Range(-rumbleAmount, rumbleAmount), Random.Range(-rumbleAmount, rumbleAmount), 0);
        }

        if (windArea.triggerOn && windOn)
        {
            windArea.playerRb.AddForce(-transform.right * windStrength);
        }
    }


    private void Timer()
    {
        if (!windOn)
        {
            timer.maxValue = cooldownTime;
            timer.currentValue += Time.deltaTime;

            if (timer.currentValue >= timer.maxValue)
            {
                timer.maxValue = blowTime;
                timer.currentValue = timer.maxValue;
                timer.fillColor = new Color(1, 0, 0);
                particles.Play();
                WindStart();
                // windOn = true;

            }
        }
        else if (windOn)
        {
            timer.currentValue -= Time.deltaTime;
            if (timer.currentValue <= timer.minValue)
            {
                timer.currentValue = timer.minValue;
                timer.maxValue = cooldownTime;
                timer.fillColor = new Color(0, 1, 0);
                particles.Stop();
                WindStop();
                // windOn = false;

            }
        }
    }

    private void WindStart()
    {
        windOn = true;
        onWindStart?.Invoke();
    }

    private void WindStop()
    {
        windOn = false;
        onWindStop?.Invoke();
    }

}
