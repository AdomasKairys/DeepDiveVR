using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public event EventHandler OnWaterEnter;
    public event EventHandler OnWaterExit;

    private bool _isInWater = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            _isInWater=true;
            OnWaterEnter?.Invoke(this, EventArgs.Empty);
        }
    }

    // Required if the trigger is composed out of multiple triggers
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera") && !_isInWater)
        {
            _isInWater = true;
            OnWaterEnter?.Invoke(this, EventArgs.Empty);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            _isInWater=false;
            OnWaterExit?.Invoke(this, EventArgs.Empty);
        }
    }

}
