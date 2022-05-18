using UnityEngine;
using FMODUnity;

public class FMODPlayOneShotWithParameter : MonoBehaviour
{

    [SerializeField] private EventReference fmodEvent;
    [SerializeField] private string parameterName;

    private FMOD.Studio.EventInstance eventInstance; //for caching the event itself
    private FMOD.Studio.PARAMETER_ID eventParameterId; //for caching the paramter id which is more efficient than setting it by name each time


    private void Start()
    {
        InitEvent();
    }

    private void InitEvent()
    {
        FMOD.Studio.EventDescription eventDescription = RuntimeManager.GetEventDescription(fmodEvent);
        FMOD.Studio.PARAMETER_DESCRIPTION eventParameterDescription;

        eventDescription.getParameterDescriptionByName(parameterName, out eventParameterDescription);
        eventParameterId = eventParameterDescription.id;
    }

    public void PlayEvent(float value)
    {
        eventInstance = RuntimeManager.CreateInstance(fmodEvent);
        eventInstance.setParameterByID(eventParameterId, value);
        eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
        eventInstance.start();
        eventInstance.release();
    }

    private void OnDestroy()
    {
        eventInstance.release();
    }

}
