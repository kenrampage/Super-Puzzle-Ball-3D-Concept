using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePause : MonoBehaviour
{
    public void ToggleTimeScale()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            print("Time is stopped");
        }
        else
        {
            Time.timeScale = 1f;
            print("Time is moving at normal speed");
        }

    }
}
