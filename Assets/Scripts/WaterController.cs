using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterController : MonoBehaviour
{
    [SerializeField] float objectMassScale = 0.2f;

    public UnityEvent OnWaterEnter;
    public UnityEvent OnWaterExit;

    public bool IsInWater { get; private set; }

    private void Awake()
    {
        IsInWater = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            IsInWater = true;
            OnWaterEnter?.Invoke();
        }
    }

    // Required if the trigger is composed out of multiple triggers
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera") && !IsInWater)
        {
            IsInWater = true;
            OnWaterEnter?.Invoke();
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
            IsInWater = false;
            GameManager.Instance.Resurface();
            OnWaterExit?.Invoke();
        }
    }

}
