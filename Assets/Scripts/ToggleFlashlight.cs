using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VLB;

public class ToggleFlashlight : MonoBehaviour
{
    private Light _flashlight;
    private VolumetricLightBeamHD _volumetricLightBeamHD;
    void Awake()
    {
        _flashlight = GetComponent<Light>();
        _volumetricLightBeamHD = GetComponent<VolumetricLightBeamHD>();
    }

    public void ToggleLight()
    {
        _flashlight.enabled = !_flashlight.enabled;
        _volumetricLightBeamHD.enabled = !_volumetricLightBeamHD.enabled;
    }
}
