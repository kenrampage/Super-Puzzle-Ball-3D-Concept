using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDown_World : MonoBehaviour
{
    public SessionDataSO sessionData;
    public WorldDatabaseSO worldDatabase;
    private TMP_Dropdown dropDown;

    public GameObject levelDropDownObject;

    private void Start()
    {
        dropDown = GetComponent<TMP_Dropdown>();
        PopulateDropdown();
    }

    public void PopulateDropdown()
    {
        dropDown.options.Clear();

        foreach (WorldSO world in worldDatabase.worlds)
        {
            dropDown.options.Add(new TMP_Dropdown.OptionData() { text = world.description });
        }

        dropDown.SetValueWithoutNotify(worldDatabase.worldIndex);
        dropDown.RefreshShownValue();

    }

}
