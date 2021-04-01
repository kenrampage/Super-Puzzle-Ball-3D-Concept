using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public SessionDataSO sessionData;
    private InputActions inputActions;

    private void Awake()
    {
        inputActions = new InputActions();
        sessionData.ResetSessionData();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

}
