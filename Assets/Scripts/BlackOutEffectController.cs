using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BlackOutEffectController : MonoBehaviour
{
    [SerializeField] Volume worldVolume;

    private Vignette _vignetteEffect;
    private ColorAdjustments _colorAdjustments;
    private void Awake()
    {
        Vignette vignette;
        ColorAdjustments colorAdjustments;
        if (worldVolume.profile.TryGet(out vignette))
        {
            _vignetteEffect = vignette;
        }
        if (worldVolume.profile.TryGet(out colorAdjustments))
        {
            _colorAdjustments = colorAdjustments;
        }
    }
    public void StartBlackOutEffect()
    {
        StopAllCoroutines();
        StartCoroutine(BlackOutEffect(0.6f, new Color(0.8f, 0.4f, 0.4f, 1f)));
    }
    public void StopBlackOutEffect()
    {
        StopAllCoroutines();
        StartCoroutine(BlackOutEffect(0.2f, new Color(1f, 1f, 1f, 1f)));
    }
    private IEnumerator BlackOutEffect(float targetIntesity, Color targetColor)
    {
        float timer = 0f;
        float originalVignetteIntesity = _vignetteEffect.intensity.value;
        Color originalColor = _colorAdjustments.colorFilter.value;

        while (timer < GameManager.Instance.GetGracePeriod())
        {
            _vignetteEffect.intensity.value = Mathf.Lerp(originalVignetteIntesity, targetIntesity, timer / GameManager.Instance.GetGracePeriod());
            _colorAdjustments.colorFilter.value = Color.Lerp(originalColor, targetColor, timer / GameManager.Instance.GetGracePeriod());
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
