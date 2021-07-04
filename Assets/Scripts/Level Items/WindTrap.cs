using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            windArea.playerRb.AddForce(windDirection * windStrength);
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
                windOn = true;

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
                windOn = false;

            }
        }
    }

}
