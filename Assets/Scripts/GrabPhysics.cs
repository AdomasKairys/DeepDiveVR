using Autodesk.Fbx;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabPhysics : MonoBehaviour
{
    [SerializeField] InputActionReference controllerGrabReference;
    [SerializeField] float radius = 0.1f;
    [SerializeField] LayerMask grabLayer;
    [SerializeField] float massScale = 1.0f;
    [SerializeField] float connectedMassScale = 1.0f;
    [SerializeField] Rigidbody connectedBody;


    private FixedJoint _fixedJoint;
    private bool _isGrabbing = false;
    private float _defaultDrag;
    private void Awake()
    {
        _defaultDrag = connectedBody.drag;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (controllerGrabReference.action.ReadValue<float>() > 0.1f && !_isGrabbing)
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, radius, grabLayer);
            if (nearbyColliders.Length > 0)
            {
                Rigidbody nearbyRigidbody = nearbyColliders[0].attachedRigidbody;

                _fixedJoint = gameObject.AddComponent<FixedJoint>();
                _fixedJoint.autoConfigureConnectedAnchor = false;
                _fixedJoint.massScale = massScale;
                _fixedJoint.connectedMassScale = connectedMassScale;

                if (nearbyRigidbody)
                {
                    _fixedJoint.connectedBody = nearbyRigidbody;
                    _fixedJoint.connectedAnchor = nearbyRigidbody.transform.InverseTransformPoint(transform.position);
                }
                else
                {
                    connectedBody.drag = 6f;
                    _fixedJoint.connectedAnchor = transform.position;
                }
                _isGrabbing = true;
            }
        }
        else if (controllerGrabReference.action.ReadValue<float>() <= 0.1f && _isGrabbing)
        {
            _isGrabbing = false;
            connectedBody.drag = _defaultDrag;
            if (_fixedJoint)
                Destroy(_fixedJoint);
        }
    }
}
