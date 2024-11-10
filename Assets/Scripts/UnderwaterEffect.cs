using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UnderwaterEffect : MonoBehaviour
{
    private bool _isInWatter = false;
    private Vector3 _initialPos = Vector3.zero;
    [SerializeField] private Transform playerTransform;
    private void Update()
    {
        if (_isInWatter)
        {
            float diff = -playerTransform.position.y > 0 ? -playerTransform.position.y * 10 : 0;
            if (diff > 100) diff = 150;
            RenderSettings.fogColor = new Color((153 - diff)/255, (192 - diff)/255, (178 - diff)/255, 1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MainCamera"))
        {
            _isInWatter = true;
            _initialPos = other.transform.position;
            //73c0b2
            RenderSettings.fogColor = new Color(153 / 255, 192 / 255, 178 / 255, 1);
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogStartDistance = 0;
            RenderSettings.fogEndDistance = 50;
            RenderSettings.fogColor = RenderSettings.fogColor;
            Debug.Log(RenderSettings.fogColor);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            _isInWatter = false;
            //A8C1E0
            RenderSettings.fogColor = new Color(168 / 255, 193 / 255, 224 / 255, 1);
            RenderSettings.fogMode = FogMode.Exponential;
            RenderSettings.fogDensity = 0.007f;
            DynamicGI.UpdateEnvironment();
        }
    }
}
