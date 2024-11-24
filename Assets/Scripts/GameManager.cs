using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float maxOxygen = 100f;
    public static GameManager Instance { get; private set; }

    public event EventHandler OnOxygenChanged;
    public event EventHandler OnOxygenRanOut;


    private float _currentOxygen = 0;
    private bool _isOutOfOxygen = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _currentOxygen = maxOxygen;
    }

    public float GetCurrentOxygen() => _currentOxygen;
    public float GetMaxOxygen() => maxOxygen;


    public void ReduceOxygen(float ammount)
    {
        if(_isOutOfOxygen) return;
        _currentOxygen -= ammount;
        if (_currentOxygen <= 0.01f)
        {
            _isOutOfOxygen = true;
            OnOxygenRanOut?.Invoke(this, EventArgs.Empty);
        }
        else
            OnOxygenChanged?.Invoke(this, EventArgs.Empty);
    }
    
}
