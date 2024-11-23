using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.Events;
using static UnityEngine.InputSystem.InputAction;

public class OneDimItemToggle : MonoBehaviour
{
    [SerializeField] UnityEvent primaryAction;
    [SerializeField] UnityEvent secondaryAction;

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.activated.AddListener(Item_OnActivated);
    }

    void OnDestroy()
    {
        grabInteractable.activated.RemoveListener(Item_OnActivated);
    }

    private void Item_OnActivated(ActivateEventArgs args)
    {
        var controller = args.interactorObject as XRBaseInputInteractor;


        if (controller != null)
        {
            // activate input needs to be with negative and positive bindings (1D)
            float dimension = controller.activateInput.ReadValue();
            Debug.Log(dimension);
            if (dimension > 0)
            {
                primaryAction.Invoke();
            }
            else if (dimension < 0)
            {
                secondaryAction.Invoke();
            }
        }
    }
}
