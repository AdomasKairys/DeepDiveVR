using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BlackOutEffectController : MonoBehaviour
{
    [SerializeField] Volume worldVolume;
    [SerializeField] Image blackOutImage;

    private Vignette _vignetteEffect;
    private void Awake()
    {
        Vignette vignette;
        if (worldVolume.profile.TryGet(out vignette))
        {
            _vignetteEffect = vignette;
        }
    }
    public void StartBlackOutEffect()
    {
        StartCoroutine(BlackOutEffect());
    }
    private IEnumerator BlackOutEffect()
    {
        float timer = 0f;
        Color color = blackOutImage.color;
        blackOutImage.enabled = true;
        color.a = 0;
        float originalColorA = color.a;
        float originalVignetteIntesity = _vignetteEffect.intensity.value;
        while(timer < GameManager.Instance.GetGracePeriod())
        {
            _vignetteEffect.intensity.value = Mathf.Lerp(originalVignetteIntesity, 0.6f, timer / GameManager.Instance.GetGracePeriod());
            color.a = Mathf.Lerp(originalColorA, (float)224/255, timer/ GameManager.Instance.GetGracePeriod());
            blackOutImage.color = color;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
