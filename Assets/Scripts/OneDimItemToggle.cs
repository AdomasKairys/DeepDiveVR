using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.Events;

public class OneDimItemToggle : MonoBehaviour
{
    [Header("Continuous action")]
    [SerializeField] UnityEvent primaryActionActivatedContinuous;
    [SerializeField] UnityEvent secondaryActionActivatedContinuous;
    [Header("One shot action")]
    [SerializeField] UnityEvent primaryActionActivatedSingle;
    [SerializeField] UnityEvent secondaryActionActivatedSingle;
    [Header("Disable action")]
    [SerializeField] UnityEvent primaryActionDeactivated;
    [SerializeField] UnityEvent secondaryActionDeactivated;
    [Header("Activation delay")]
    [SerializeField, Min(0f)] float activationDelay = 0.1f; 

    private XRGrabInteractable _grabInteractable;
    private int _toggleValue = 0;

    private float _currentDelay = 0f;
    void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();

        _grabInteractable.activated.AddListener(Item_OnActivated);
        _grabInteractable.deactivated.AddListener(Item_OnDeactivated);
        _grabInteractable.selectExited.AddListener((_) =>
        {
            _toggleValue = 0;
            primaryActionDeactivated.Invoke();
            primaryActionDeactivated.Invoke();
        });
    }
    private void Update()
    {
        if (_currentDelay > 0.01f)
        {
            _currentDelay -= Time.deltaTime;
            return;
        }

        if(_toggleValue == 0)
            return;

        _currentDelay = activationDelay;
        if (_toggleValue > 0)
        {
            primaryActionActivatedContinuous.Invoke();
        }
        else if (_toggleValue < 0)
        {
            secondaryActionActivatedContinuous.Invoke();
        }
    }
    void OnDestroy()
    {
        _grabInteractable.activated.RemoveListener(Item_OnActivated);
        _grabInteractable.deactivated.RemoveListener(Item_OnDeactivated);
    }
    private void Item_OnActivated(ActivateEventArgs args)
    {
        var controller = args.interactorObject as XRBaseInputInteractor;


        if (controller != null)
        {
            // activate input needs to be with negative and positive bindings (1D)
            float dimension = controller.activateInput.ReadValue();
            _toggleValue = (int)dimension;

            if (_toggleValue > 0)
            {
                primaryActionActivatedSingle.Invoke();
            }
            else if (_toggleValue < 0)
            {
                secondaryActionActivatedSingle.Invoke();
            }
            //Debug.Log(dimension);
        }
    }
    private void Item_OnDeactivated(DeactivateEventArgs args)
    {
        var controller = args.interactorObject as XRBaseInputInteractor;

        if (controller != null)
        {
            // activate input needs to be with negative and positive bindings (1D)
            float dimension = controller.activateInput.ReadValue();
            _toggleValue = (int)dimension;

            primaryActionDeactivated.Invoke();
            primaryActionDeactivated.Invoke();
            //Debug.Log(dimension);
        }
    }
}
