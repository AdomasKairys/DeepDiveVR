using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public event EventHandler OnWaterEnter;
    public event EventHandler OnWaterExit;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            OnWaterEnter?.Invoke(this, EventArgs.Empty);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            var rb = other.attachedRigidbody;
            rb.AddForce(-Physics.gravity * 0.9f * rb.mass);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            OnWaterExit?.Invoke(this, EventArgs.Empty);
        }
    }

}
