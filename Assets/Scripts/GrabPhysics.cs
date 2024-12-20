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
    private float _defaultDrag;
    public bool IsGrabbing { get; private set; } = false;
    private void Awake()
    {
        _defaultDrag = connectedBody.drag;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controllerGrabReference.action.ReadValue<float>() > 0.1f && !IsGrabbing)
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
                IsGrabbing = true;
            }
        }
        else if (controllerGrabReference.action.ReadValue<float>() <= 0.1f && IsGrabbing)
        {
            IsGrabbing = false;
            connectedBody.drag = _defaultDrag;
            if (_fixedJoint)
                Destroy(_fixedJoint);
        }
    }
}
