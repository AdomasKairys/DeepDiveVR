using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class HandPhysics : MonoBehaviour
{
    [SerializeField] Transform target;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.velocity = (target.position-transform.position)/Time.fixedDeltaTime;

        Quaternion rotationDiff = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDiff.ToAngleAxis(out float angle, out Vector3 rotationAxis);

        Vector3 rotationDiffDegree = angle * rotationAxis;

        rb.angularVelocity= (rotationDiffDegree*Mathf.Deg2Rad)/Time.fixedDeltaTime;
    }
}
