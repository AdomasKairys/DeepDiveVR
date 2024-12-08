using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class OxygenRefillController : MonoBehaviour
{
    void Awake()
    {
        XRSimpleInteractable xRSimpleInteractable = gameObject.GetComponent<XRSimpleInteractable>();

        xRSimpleInteractable.selectEntered.AddListener((_) =>
        {
            GameManager.Instance.RefillOxygen();
        });
    }
}
