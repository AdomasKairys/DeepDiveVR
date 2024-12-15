using AtmosphericHeightFog;
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
    [SerializeField] HeightFogOverride heightFogOverride;
    [SerializeField] Transform mainCamera;
    private bool _isInWatter = false;
    private Vector3 _playerPostition = Vector3.zero;
    private void Update()
    {
        if(_isInWatter)
            heightFogOverride.directionalIntensity = Mathf.Clamp(mainCamera.transform.position.y/40, 0, 1);
    }
    public void Water_OnExit()
    {
        _isInWatter = false;

        //9FA6CF
        //RenderSettings.fogColor = new Color(0x9F / 255, 0xA6 / 255, 0xCF / 255, 1);
        //RenderSettings.fogMode = UnityEngine.FogMode.Exponential;
        //RenderSettings.fogDensity = 0.003f;
        //RenderSettings.fogColor = RenderSettings.fogColor;
        //Debug.Log(RenderSettings.fogColor);

    }

    public void Water_OnEnter()
    {
        _isInWatter = true;
        //73c0b2
        //RenderSettings.fogColor = Color.black;
        //RenderSettings.fogMode = UnityEngine.FogMode.Linear;
        //RenderSettings.fogStartDistance = 0;
        //RenderSettings.fogEndDistance = 50;
        //RenderSettings.fogColor = RenderSettings.fogColor;
        //Debug.Log(RenderSettings.fogColor);
    }
}
