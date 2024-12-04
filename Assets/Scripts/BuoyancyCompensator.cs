using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuoyancyCompensator : MonoBehaviour
{
    [Tooltip("reference of the water surface")]
    [SerializeField] Transform waterSurfaceLevel;
    [SerializeField] Rigidbody playerRigidBody;

    [SerializeField] WaterController waterController;

    //[SerializeField] float buoyancyStrenghtScale = 1f;
    [SerializeField] float compensatorInflateSpeed = 0.05f;
    [SerializeField] float compensatorDeflateSpeed = 0.1f;
    [SerializeField] float maxCompensatorInflation = 1f;
    [SerializeField] float underwaterMassScale = 0.1f;
    [SerializeField] float maxBuoyancyForce = Physics.gravity.magnitude*0.25f;
    [Tooltip("compensator is compressed by compressAmmount every time y value decreases by waterPressureStep")]
    [SerializeField] float waterPressureStep = 2f;
    [Tooltip("compenstor copression scale when going deeper")]
    [SerializeField] float compressAmmount = 0.1f;

    private Vector3 _buoyancyPoint = Vector3.zero;

    // if buoyancy force is greater than the force of gravity we have positive buoyancy (rising)
    // if its less than the force of gravity we have negative buoyancy (sinking)
    // if its equalt to the force of gravity we have neutral buoyancy (floating in place)
    private float _buoyancyForce = 0f;

    // from 0 to 1
    private float _currentInflation = 0f;

    private float _volumeOfAir = 0f;

    private void Awake()
    {
        waterController.OnWaterEnter += Water_OnEnter;
        waterController.OnWaterExit += Water_OnExit;
        this.enabled = false;
    }
    private void Update()
    {
        //if (inflateCompensatorReference.action.IsPressed() && _currentInflation < maxCompensatroInflation)
        //{
        //    _volumeOfAir += compensatorInflateSpeed;
        //}
        //else if(deflateCompensatorReference.action.IsPressed() && _currentInflation > 0)
        //{
        //    _volumeOfAir -= compensatorDeflateSpeed;
        //}

        _currentInflation = _volumeOfAir - compressAmmount * Mathf.Round(waterSurfaceLevel.position.y-playerRigidBody.position.y) / waterPressureStep;
        _currentInflation = _currentInflation < 0 ? 0 : _currentInflation;
        _buoyancyForce = _currentInflation * maxBuoyancyForce;
    }

    void FixedUpdate()
    {
        //var direction = (transform.position - _buoyancyPoint).magnitude > 1 ? (transform.position - _buoyancyPoint).normalized : transform.position - _buoyancyPoint;
        playerRigidBody.AddForce((-Physics.gravity * (1-underwaterMassScale)) + new Vector3(0,_buoyancyForce), ForceMode.Acceleration);
    }
    private void Water_OnEnter(object sender, EventArgs e)
    {
        this.enabled = true;
    }
    private void Water_OnExit(object sender, EventArgs e)
    {
        this.enabled = false;
    }
    public void Inflate()
    {
        if(_currentInflation < maxCompensatorInflation)
        {
            _volumeOfAir += compensatorInflateSpeed;
        }
    }
    public void Deflate()
    {
        if (_currentInflation > 0)
        {
            _volumeOfAir -= compensatorDeflateSpeed;
        }
    }
    public float GetCurrentInflation() => _currentInflation;
}
