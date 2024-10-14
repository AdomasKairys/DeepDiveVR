using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

public class HeadCollisionHandler : MonoBehaviour
{
    [SerializeField] HeadCollisionDetector detector;
    [SerializeField] Rigidbody player;
    [SerializeField] float pushBackStrength = 1.0f;

    private void Update()
    {
        if(detector.DetectedColliderHits.Count <= 0)
            return;
        Vector3 pushBackDirection = CalculatePushBackDirection(detector.DetectedColliderHits);

        player.AddForce(pushBackDirection * pushBackStrength, ForceMode.Force);

    }

    private Vector3 CalculatePushBackDirection(List<RaycastHit> colliderHits)
    {
        Vector3 combinedNormal = Vector3.zero;
        foreach (RaycastHit hit in colliderHits)
        {
            combinedNormal += new Vector3(hit.normal.x, 0, hit.normal.z);
        }
        return combinedNormal;
    }
}
