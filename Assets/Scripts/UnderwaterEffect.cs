using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UnderwaterEffect : MonoBehaviour
{
    private bool _isInWatter = false;
    private Vector3 _playerPostition = Vector3.zero;

    private void Awake()
    {
        var waterController = GetComponent<WaterController>();

        waterController.OnWaterEnter += Water_OnEnter;
        waterController.OnWaterExit += Water_OnExit;
    }

    

    private void Update()
    {
        if (false)
        {
            float diff = -_playerPostition.y > 0 ? -_playerPostition.y * 10 : 0;
            if (diff > 150) diff = 150;
            RenderSettings.fogColor = new Color((153 - diff)/255, (192 - diff)/255, (178 - diff)/255, 1);
        }
    }

    private void Water_OnExit(object sender, EventArgs e)
    {
        _isInWatter = false;
        //A8C1E0
        RenderSettings.fogColor = new Color(168 / 255, 193 / 255, 224 / 255, 1);
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = 0.007f;
        DynamicGI.UpdateEnvironment();
    }

    private void Water_OnEnter(object sender, EventArgs e)
    {
        _isInWatter = true;
        //73c0b2
        RenderSettings.fogColor = new Color(153 / 255, 192 / 255, 178 / 255, 1);
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 0;
        RenderSettings.fogEndDistance = 50;
        RenderSettings.fogColor = RenderSettings.fogColor;
        Debug.Log(RenderSettings.fogColor);
    }
}
