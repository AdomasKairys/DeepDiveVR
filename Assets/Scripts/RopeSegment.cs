using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    [SerializeField] private GameObject connectedAbove, connectedBellow;
    void Start()
    {
        connectedAbove = GetComponent<HingeJoint>().connectedBody.gameObject;
        RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();
        if(aboveSegment != null)
        {
            aboveSegment.connectedBellow = gameObject;
            float meshBottom = connectedAbove.GetComponent<MeshRenderer>().bounds.size.y;
            GetComponent<HingeJoint>().connectedAnchor = new Vector3(0, meshBottom * -1);
            GetComponent<HingeJoint>().anchor = new Vector3(0, meshBottom * 0.5f);
        }
        else
        {
            GetComponent<HingeJoint>().connectedAnchor = Vector3.zero;
        }
    }
}
