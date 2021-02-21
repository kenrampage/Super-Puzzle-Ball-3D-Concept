using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulateLevelDropdown : MonoBehaviour
{

    public TMP_Dropdown dropDown;

    private void Start()
    {
        dropDown = GetComponent<TMP_Dropdown>();
        //dropDown.ClearOptions();
    }

    public void PopulateDropdown()
    {
        if (dropDown.options.Count > 1)
        {

        }
        else
        {
            foreach (string t in GameManager.Instance.scenesInBuild)
            {
                if(t != "MainMenu")
                dropDown.options.Add(new TMP_Dropdown.OptionData() { text = t });
            }
        }

    }

}
