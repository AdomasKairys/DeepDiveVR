using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRig : MonoBehaviour
{
    [SerializeField] Transform playerHead;
    [SerializeField] Transform leftController;
    [SerializeField] Transform rightController;

    [SerializeField] ConfigurableJoint headJoint;
    [SerializeField] ConfigurableJoint leftHandJoint;
    [SerializeField] ConfigurableJoint rightHandJoint;

    [SerializeField] CapsuleCollider bodyCollider;

    [SerializeField] float bodyHeightMin = 0.5f;
    [SerializeField] float bodyHeightMax = 2f;

    private void FixedUpdate()
    {
        bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y, bodyHeightMin, bodyHeightMax);
        bodyCollider.center = new Vector3 (playerHead.localPosition.x, bodyCollider.height / 2,
            playerHead.localPosition.z);


        leftHandJoint.targetPosition = leftController.localPosition;
        leftHandJoint.targetRotation = leftController.localRotation;

        rightHandJoint.targetPosition = rightController.localPosition;
        rightHandJoint.targetRotation = rightController.localRotation;

        headJoint.targetPosition = playerHead.localPosition;
    }

}
