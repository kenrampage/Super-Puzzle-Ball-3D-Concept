using UnityEngine;
using UnityEngine.Events;

public class SOIntegerHelper : MonoBehaviour
{
    [SerializeField] private SOInteger soInteger;
    
    [SerializeField] private bool resetValueOnAwake;

    [SerializeField] private UnityEvent<int> onValueChanged;
    [SerializeField] private UnityEvent onMinValueMet;
    [SerializeField] private UnityEvent onMaxValueMet;
    [SerializeField] private UnityEvent onValueReset;

    

    private void Awake()
    {
        if(resetValueOnAwake)
        {
            soInteger.ResetValue();
        }
    }

    private void OnEnable()
    {
        soInteger.onValueChanged += HandleValueChanged;
        soInteger.onMinValueMet += HandleMinValueMet;
        soInteger.onMaxValueMet += HandleMaxValueMet;
        soInteger.onValueReset += HandleValueReset;
    }

    private void OnDisable()
    {
        soInteger.onValueChanged -= HandleValueChanged;
        soInteger.onMinValueMet -= HandleMinValueMet;
        soInteger.onMaxValueMet -= HandleMaxValueMet;
        soInteger.onValueReset -= HandleValueReset;
    }

    private void HandleValueChanged(int value)
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
