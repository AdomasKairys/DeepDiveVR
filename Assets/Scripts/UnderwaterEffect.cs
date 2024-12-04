using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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

    private void Water_OnExit(object sender, EventArgs e)
    {
        _isInWatter = false;

        //9FA6CF
        RenderSettings.fogColor = new Color(0x9F / 255, 0xA6 / 255, 0xCF / 255, 1);
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = 0.003f;
        RenderSettings.fogColor = RenderSettings.fogColor;
        Debug.Log(RenderSettings.fogColor);

    }

    private void Water_OnEnter(object sender, EventArgs e)
    {
        _isInWatter = true;
        //73c0b2
        RenderSettings.fogColor = Color.black;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 0;
        RenderSettings.fogEndDistance = 50;
        RenderSettings.fogColor = RenderSettings.fogColor;
        Debug.Log(RenderSettings.fogColor);
    }
}
