using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDown_Level : MonoBehaviour
{
    public SessionDataSO sessionData;
    public WorldDatabaseSO worldDatabase;
    private TMP_Dropdown dropDown;

    private void Start()
    {
        dropDown = GetComponent<TMP_Dropdown>();
        PopulateDropdown();
    }

    public void PopulateDropdown()
    {

        dropDown.options.Clear();

        foreach (LevelSO t in worldDatabase.GetCurrentWorld().levels)
        {
            dropDown.options.Add(new TMP_Dropdown.OptionData() { text = t.levelName });
        }

        dropDown.SetValueWithoutNotify(worldDatabase.GetCurrentWorld().levelIndex);
        dropDown.RefreshShownValue();
        
    }


}
