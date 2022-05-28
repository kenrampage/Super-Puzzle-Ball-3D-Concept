
using UnityEngine;

public class SetFMODParameterValue : MonoBehaviour
{
    [SerializeField] private SOFMODParameterData fmodParameterData;


    public void IncrementValue()
    {
        fmodParameterData.FloatValue = fmodParameterData.FloatValue + 1;
    }

    public void ResetValue()
    {
        fmodParameterData.FloatValue = 0;
    }
}
