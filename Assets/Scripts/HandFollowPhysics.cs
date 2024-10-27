using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFollowPhysics : MonoBehaviour
{
    [SerializeField] Transform parent;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        _rb.AddForce(20f*(transform.position - parent.position), ForceMode.Acceleration);
    }
}
