using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadLevelButton : MonoBehaviour
{
    public SO_SessionData sessionData;

    public void LoadButtonClicked()
    {
        GameManager.Instance.LoadLevel(sessionData.nextLevelName);
        sessionData.currentLevelName = sessionData.nextLevelName;
        sessionData.nextLevelName = string.Empty;
    }

}
