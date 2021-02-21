using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadLevelButton : MonoBehaviour
{

    public TMP_Dropdown dropDown;

    public void LoadButtonClicked()
    {
        if(dropDown.value > 0)
        GameManager.Instance.LoadLevel(dropDown.options[dropDown.value].text);
    }

}
