using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] public int maximumShield;
    private int _currentShield;
    public static event Action OnShieldChanged;

    public void ResetState()
    {
        CurrentShield = maximumShield;
        OnShieldChanged?.Invoke();
    }

    public int CurrentShield
    {
        get { return _currentShield; }
        private set { _currentShield = value; }
    }

    public void DeductShield(int amount)
    {
        _currentShield = Mathf.Clamp(_currentShield - amount, 0, maximumShield);
        OnShieldChanged?.Invoke();
        if (_currentShield == 0)
        {
            // Destroy(this);
            enabled = false;
        }
    }
}