using UnityEngine.InputSystem;
using UnityEngine;
using System;
using Unity.Mathematics;

[RequireComponent(typeof(Rigidbody))]
public class Swimming : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] float swimForce = 2f;
    [SerializeField] float dragForce = 1f;
    [SerializeField] float minForce;
    [SerializeField] float minTimeBetweenStrokes;
    [Header("References")]
    [SerializeField] InputActionReference leftControllerSwingReference;
    [SerializeField] InputActionReference leftControllerVelocity;
    [SerializeField] InputActionReference rightControllerSwingReference;
    [SerializeField] InputActionReference rightControllerVelocity;
    [SerializeField] Transform trackingReference;
    [SerializeField] CapsuleCollider capsuleCollider;

    //[SerializeField] Transform leftController;
    //[SerializeField] Transform rightController;


    Rigidbody _rigidbody;

    float _coolDownTimer;

    int _audioClipsLength;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void FixedUpdate()
    {
        _coolDownTimer += Time.fixedDeltaTime;
        if (_coolDownTimer > minTimeBetweenStrokes
            && leftControllerSwingReference.action.IsPressed()
            && rightControllerSwingReference.action.IsPressed())
        {
            var leftHandVelocity = leftControllerVelocity.action.ReadValue<Vector3>();
            var rightHandVelocity = rightControllerVelocity.action.ReadValue<Vector3>();


            Vector3 localVelocity = leftHandVelocity + rightHandVelocity;
            localVelocity *= -1;

            if (localVelocity.sqrMagnitude > minForce * minForce)
            {
                Vector3 worldVelocity = trackingReference.TransformDirection(localVelocity);
                _rigidbody.AddForce(worldVelocity * swimForce, ForceMode.Acceleration);
                _coolDownTimer = 0;
            }
        }
        if(_rigidbody.velocity.sqrMagnitude > 0.01f)
        {
            _rigidbody.AddForce(-_rigidbody.velocity * dragForce, ForceMode.Acceleration);
        }
    }
}
