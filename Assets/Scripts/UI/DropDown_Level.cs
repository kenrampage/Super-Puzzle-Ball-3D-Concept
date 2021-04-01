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

        foreach (string t in worldDatabase.CurrentWorld().levels)
        {
            dropDown.options.Add(new TMP_Dropdown.OptionData() { text = t });
        }

        dropDown.SetValueWithoutNotify(worldDatabase.CurrentWorld().levelIndex);
        dropDown.RefreshShownValue();
        
    }


}
