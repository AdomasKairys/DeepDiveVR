using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRig : MonoBehaviour
{
    [SerializeField] Transform playerHead;
    [SerializeField] Transform leftController;
    [SerializeField] Transform rightController;
    [SerializeField] Transform cameraOffset;
    [SerializeField] GrabPhysics rightGrabPhysics;
    [SerializeField] GrabPhysics leftGrabPhysics;


    [SerializeField] ConfigurableJoint headJoint;
    [SerializeField] ConfigurableJoint leftHandJoint;
    [SerializeField] ConfigurableJoint rightHandJoint;

    [SerializeField] CapsuleCollider bodyCollider;

    [SerializeField] float bodyHeightMin = 0.5f;
    [SerializeField] float bodyHeightMax = 2f;

    [SerializeField] WaterController waterController;

    private bool _resetHeight = false;

    private void FixedUpdate()
    {
        // not clean code but whatever
        if (!waterController.IsInWater && !rightGrabPhysics.IsGrabbing && !leftGrabPhysics.IsGrabbing)
        {
            if(_resetHeight)
                StartCoroutine(ResetPlayerHeight());
            else
            {
                bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y + cameraOffset.localPosition.y, bodyHeightMin, bodyHeightMax);
                bodyCollider.center = new Vector3(playerHead.localPosition.x, bodyCollider.height / 2, playerHead.localPosition.z);
            }
        }
        else
        {
            _resetHeight = true;
            StopAllCoroutines();
            bodyCollider.height = 0.5f;
        }


        leftHandJoint.targetPosition = leftController.localPosition;
        leftHandJoint.targetRotation = leftController.localRotation;

        rightHandJoint.targetPosition = rightController.localPosition;
        rightHandJoint.targetRotation = rightController.localRotation;

        headJoint.targetPosition = playerHead.localPosition;
    }
    private IEnumerator ResetPlayerHeight()
    {
        float resetDuration = 2f;
        float currentDuration = 0f;

        float targetValue = Mathf.Clamp(playerHead.localPosition.y + cameraOffset.localPosition.y, bodyHeightMin, bodyHeightMax);
        float currentValue = bodyCollider.height;

        while (currentDuration < resetDuration) 
        {
            bodyCollider.height = Mathf.Lerp(currentValue, targetValue, currentDuration / resetDuration);
            bodyCollider.center = new Vector3(playerHead.localPosition.x, bodyCollider.height / 2, playerHead.localPosition.z);
            currentDuration += Time.deltaTime;
            yield return null;
        }
    }

}
