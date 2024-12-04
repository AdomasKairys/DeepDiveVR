using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuoyancyCompensatorUIController : MonoBehaviour
{
    [SerializeField] Slider buoyancySlider;
    [SerializeField] BuoyancyCompensator buoyancyCompensator;

    void Update()
    {
        buoyancySlider.value = buoyancyCompensator.GetCurrentInflation();
    }
}
