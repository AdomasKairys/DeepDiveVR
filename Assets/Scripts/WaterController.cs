using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    [SerializeField] float objectMassScale = 0.2f;

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
        if (other.CompareTag("Object"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(-Physics.gravity * (1-objectMassScale), ForceMode.Acceleration);
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
