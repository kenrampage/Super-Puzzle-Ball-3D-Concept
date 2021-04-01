using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleTimerButton : MonoBehaviour
{
    public SessionDataSO sessionData;
    public TextMeshProUGUI buttonText;


    private void Start()
    {
        UpdateButtonText();
    }

    public void ToggleTimerActive()
    {
        sessionData.timerIsActive = !sessionData.timerIsActive;

        UpdateButtonText();
    }

    public void UpdateButtonText()
    {
        buttonText.text = "Timer Active: " + sessionData.timerIsActive;
    }

}
