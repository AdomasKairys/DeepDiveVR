using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenController : MonoBehaviour
{
    [Min(0f)]
    [SerializeField] float breathingSpeed = 1;

    void Update()
    {
        GameManager.Instance.ReduceOxygen(breathingSpeed * Time.deltaTime);
    }
}
