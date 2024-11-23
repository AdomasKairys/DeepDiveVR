using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopePhysicsToggle : MonoBehaviour
{
    [SerializeField] Rigidbody[] ropeBones;

    private void Awake()
    {
        Sleep();
    }

    public void Sleep()
    {
        foreach (var bone in ropeBones)
        {
            bone.Sleep();
        }
    }
    public void WakeUp()
    {
        foreach (var bone in ropeBones)
        {
            bone.WakeUp();
        }
    }
}
