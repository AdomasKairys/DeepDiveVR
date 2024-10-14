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

    private FixedJoint _fixedJoint;
    private bool _isGrabbing = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (controllerGrabReference.action.IsPressed() && !_isGrabbing)
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, radius, grabLayer);
            if (nearbyColliders.Length > 0)
            {
                Rigidbody nearbyRigidbody = nearbyColliders[0].attachedRigidbody;

                _fixedJoint = gameObject.AddComponent<FixedJoint>();

                _fixedJoint.autoConfigureConnectedAnchor = false;

                if (nearbyRigidbody)
                {
                    _fixedJoint.connectedBody = nearbyRigidbody;
                    _fixedJoint.connectedAnchor = nearbyRigidbody.transform.InverseTransformPoint(transform.position);
                }
                else
                {
                    _fixedJoint.connectedAnchor = transform.position;
                }
                _isGrabbing = true;
            }
        }
        else if (!controllerGrabReference.action.IsPressed() && _isGrabbing)
        {
            _isGrabbing = false;
            if (_fixedJoint)
                Destroy(_fixedJoint);
        }
    }
}
