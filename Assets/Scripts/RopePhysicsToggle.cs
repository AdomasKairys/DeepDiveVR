using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePhysicsToggle : MonoBehaviour
{
    [SerializeField] Rigidbody[] ropeBones;

    private Transform[] _ropeTransforms;
    private void Awake()
    {
        _ropeTransforms = new Transform[ropeBones.Length];
        for (int i = 0; i < ropeBones.Length; i++)
        {
            _ropeTransforms[i] = ropeBones[i].transform;
        }
    }
    private void Start()
    {
        Sleep();
    }
    private IEnumerator SleepCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < ropeBones.Length; i++)
        {
            ropeBones[i].isKinematic = true;
            ropeBones[i].transform.position = _ropeTransforms[i].position;
            ropeBones[i].rotation = _ropeTransforms[i].rotation;
        }
    }
    public void Sleep()
    {
        Debug.Log("should");
        StopAllCoroutines();
        StartCoroutine(SleepCoroutine());
    }
    public void WakeUp()
    {
        Debug.Log("not");
        StopAllCoroutines();
        for (int i = 0; i < ropeBones.Length; i++)
        {
            ropeBones[i].isKinematic = false;
        }
    }
}
