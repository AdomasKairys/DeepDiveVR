using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OxygenUIController : MonoBehaviour
{
    [SerializeField] Image fillImage;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI pressureText;
    [SerializeField] GameObject warningUI;

    [SerializeField, Range(0f, 1f)] float colorChangeRatio;
    [SerializeField, Tooltip("Pressure in bar, only visual")] float maxPressure = 230f;
    [SerializeField, Tooltip("Value at which a warning symbol appears")] float warningValue = 10f;

    private Color _initialColor;
    void Start()
    {
        slider.value = 1;
        pressureText.text = maxPressure.ToString("0.0");
        _initialColor = fillImage.color;
    }

    public void UpdateOxygenUI()
    {
        slider.value = GameManager.Instance.GetCurrentOxygen() / GameManager.Instance.GetMaxOxygen();
        Color.RGBToHSV(_initialColor, out float h, out float s, out float v);
        fillImage.color = Color.HSVToRGB(h - colorChangeRatio*(1 - slider.value), s,v);
        pressureText.text = (maxPressure * slider.value).ToString("0.0");

        // could be moved to an event but currently no such event exists
        if(GameManager.Instance.GetCurrentOxygen() <= warningValue)
        {
            warningUI.SetActive(true);
        }
        else
        {
            warningUI.SetActive(false);
        }
    }
}
