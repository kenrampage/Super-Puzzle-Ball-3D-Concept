using UnityEngine;
using UnityEngine.Events;

public class SOFloatHelper : MonoBehaviour
{
    [SerializeField] private SOFloat soFloat;
    
    [SerializeField] private bool resetValueOnAwake;

    [SerializeField] private UnityEvent<float> onValueChanged;
    [SerializeField] private UnityEvent onMinValueMet;
    [SerializeField] private UnityEvent onMaxValueMet;
    [SerializeField] private UnityEvent onValueReset;

    

    private void Awake()
    {
        if(resetValueOnAwake)
        {
            soFloat.ResetValue();
        }
    }

    private void OnEnable()
    {
        soFloat.onValueChanged += HandleValueChanged;
        soFloat.onMinValueMet += HandleMinValueMet;
        soFloat.onMaxValueMet += HandleMaxValueMet;
        soFloat.onValueReset += HandleValueReset;
    }

    private void OnDisable()
    {
        soFloat.onValueChanged -= HandleValueChanged;
        soFloat.onMinValueMet -= HandleMinValueMet;
        soFloat.onMaxValueMet -= HandleMaxValueMet;
        soFloat.onValueReset -= HandleValueReset;
    }

    private void HandleValueChanged(float value)
    {
        onValueChanged?.Invoke(value);
    }

    private void HandleMinValueMet()
    {
        onMinValueMet?.Invoke();
    }

    private void HandleMaxValueMet()
    {
        onMaxValueMet?.Invoke();
    }

    private void HandleValueReset()
    {
        onValueReset?.Invoke();
    }




}
