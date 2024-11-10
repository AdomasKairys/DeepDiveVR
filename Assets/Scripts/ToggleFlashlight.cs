using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    private Light _flashlight;
    void Awake()
    {
        _flashlight = GetComponent<Light>();
    }

    public void ToggleLight()
    {
        _flashlight.enabled = !_flashlight.enabled;
    }
}
