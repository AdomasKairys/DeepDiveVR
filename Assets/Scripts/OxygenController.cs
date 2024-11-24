using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenController : MonoBehaviour
{

    [SerializeField] WaterController waterController;

    [Min(0f)]
    [SerializeField] float breathingSpeed = 1;


    private bool _isInWater = false;
    void Awake()
    {
        waterController.OnWaterEnter += Water_OnEnter;
        waterController.OnWaterExit += Water_OnExit;
    }

    private void Water_OnExit(object sender, System.EventArgs e) => _isInWater = false;

    private void Water_OnEnter(object sender, System.EventArgs e) => _isInWater = true;

    void Update()
    {
        if (!_isInWater)
            return;

        GameManager.Instance.ReduceOxygen(breathingSpeed * Time.deltaTime);
    }
}
