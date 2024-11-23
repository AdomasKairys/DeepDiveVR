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
    void Awake()
    {
        Instance = this;
        _currentOxygen= maxOxygen;
    }

    public float GetCurrentOxygen() => _currentOxygen;

    public void ReduceOxygen(float ammount)
    {
        _currentOxygen -= ammount;
        if (_currentOxygen <= 0.01f)
            OnOxygenRanOut?.Invoke(this, EventArgs.Empty);
        else
            OnOxygenChanged?.Invoke(this, EventArgs.Empty);
    }
    
}
