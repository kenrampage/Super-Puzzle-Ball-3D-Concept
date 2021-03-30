using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelDropdown : MonoBehaviour
{
    public SO_SessionData sessionData;
    private TMP_Dropdown dropDown;


    public void PopulateDropdown()
    {
        dropDown = GetComponent<TMP_Dropdown>();
        dropDown.ClearOptions();
        SO_LevelSet levelSet = sessionData.levelSetLibrary.setList[sessionData.currentLevelSetIndex];

        if (dropDown.options.Count > 1)
        {

        }
        else
        {
            foreach (string t in levelSet.levelList)
            {
                dropDown.options.Add(new TMP_Dropdown.OptionData() { text = t});
            }
        }

        sessionData.nextLevelName = dropDown.options[dropDown.value].text;

    }

    public void SetLevelToLoad()
    {
        sessionData.nextLevelName = dropDown.options[dropDown.value].text;
    }

}
