using UnityEngine.InputSystem;
using UnityEngine;
using System;

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
    [SerializeField] WaterController waterController;
    [SerializeField] CapsuleCollider capsuleCollider;

    //[SerializeField] Transform leftController;
    //[SerializeField] Transform rightController;


    Rigidbody _rigidbody;

    float _coolDownTimer;
    private bool _isEnabled = false;
    private float _defaultColliderHeight;

    private void Awake()
    {
        _defaultColliderHeight= capsuleCollider.height;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        waterController.OnWaterEnter += Water_OnEnter;
        waterController.OnWaterExit += Water_OnExit;

    }

    private void FixedUpdate()
    {

        if (!_isEnabled)
            return;
        
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
    private void Water_OnEnter(object sender, EventArgs e)
    {
        _isEnabled = true;
        capsuleCollider.height = 0.5f;

    }
    private void Water_OnExit(object sender, EventArgs e)
    {
        _isEnabled = false;
        capsuleCollider.height = _defaultColliderHeight;
    }
}
