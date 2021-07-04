using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float minValue;
    public float maxValue;
    public float currentValue;

    public Image background;
    public Color backgroundColor;
    
    public Image fill;
    public Color fillColor;


    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float currentOffset = currentValue - minValue;
        float maxOffset = maxValue - minValue;
        float fillAmount = currentOffset / maxOffset;
        fill.fillAmount = fillAmount;

        fill.color = fillColor;
        background.color = backgroundColor;
    }
}
