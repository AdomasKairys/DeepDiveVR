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

    [SerializeField, Range(0f, 1f)] float colorChangeRatio;
    [SerializeField, Tooltip("Pressure in bar")] float maxPressure = 230f;

    private Color _initialColor;
    void Start()
    {
        GameManager.Instance.OnOxygenChanged += GameManager_OnOxygenChanged;
        slider.value = 1;
        pressureText.text = maxPressure.ToString("0.0") + "bar";
        _initialColor = fillImage.color;
    }

    private void GameManager_OnOxygenChanged(object sender, System.EventArgs e)
    {
        slider.value = GameManager.Instance.GetCurrentOxygen() / GameManager.Instance.GetMaxOxygen();
        Color.RGBToHSV(_initialColor, out float h, out float s, out float v);
        fillImage.color = Color.HSVToRGB(h - colorChangeRatio*(1 - slider.value), s,v);
        pressureText.text = (maxPressure * slider.value).ToString("0.0") + "bar";
    }

    void Update()
    {
        //GameManager.Instance.ReduceOxygen(Time.deltaTime*10);
    }
}
