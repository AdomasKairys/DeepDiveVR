using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuoyancyCompensatorUIController : MonoBehaviour
{
    [SerializeField] Slider buoyancySlider;
    [SerializeField] BuoyancyCompensator buoyancyCompensator;

    private void Awake()
    {
        buoyancyCompensator.OnBuoyancyChanged += (_, args) => buoyancySlider.value = args.currentInflation;
    }
}
