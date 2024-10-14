using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionDetector : MonoBehaviour
{
    [SerializeField, Range(0, 0.5f)] float detectionDelay = 0.05f;
    [SerializeField] float detectionDistance = 0.2f;
    [SerializeField] LayerMask detectionLayers;
    public List<RaycastHit> DetectedColliderHits {  get; private set; }
    float _currentTime = 0;

    private void Start()
    {
        DetectedColliderHits = PerformDetection(transform.position, detectionDistance, detectionLayers);
    }
    private void Update()
    {
        _currentTime += Time.deltaTime;
        if(_currentTime > detectionDelay)
        {
            _currentTime = 0;
            DetectedColliderHits = PerformDetection(transform.position, detectionDistance, detectionLayers);
        }
    }
    private List<RaycastHit> PerformDetection(
        Vector3 position, float distance, LayerMask mask)
    {
        List<RaycastHit> detectedHits = new();

        List<Vector3> directions = new() { transform.forward, transform.right, -transform.right };

        RaycastHit hit;
        foreach (var dir in directions)
            if(Physics.Raycast(position,dir, out hit, distance, mask))
                detectedHits.Add(hit);
        
        return detectedHits;
    }

   
}
