using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSetDropdown : MonoBehaviour
{
    public SO_SessionData sessionData;
    private TMP_Dropdown dropDown;

    public GameObject levelDropDownObject;

    private void Start()
    {
        dropDown = GetComponent<TMP_Dropdown>();
    }

    public void PopulateDropdown()
    {
        if (dropDown.options.Count > 1)
        {

        }
        else
        {
            foreach (SO_LevelSet levelSet in sessionData.levelSetLibrary.setList)
            {
                dropDown.options.Add(new TMP_Dropdown.OptionData() { text = levelSet.description });
            }
        }

    }

    public void HandleValueChanged()
    {
        sessionData.SetCurrentLevelSetIndex(dropDown.value - 1);

        if(levelDropDownObject.activeSelf == false)
        {
            levelDropDownObject.SetActive(true);
        }


    }

}
