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
        float timer = 0;
        Color color = blackOutImage.color;
        color.a = 0;
        while(timer < GameManager.Instance.GetGracePeriod())
        {
            _vignetteEffect.intensity.value = Mathf.Lerp(_vignetteEffect.intensity.value, 0.6f, timer / GameManager.Instance.GetGracePeriod());
            color.a = Mathf.Lerp(color.a, 224/255, timer/ GameManager.Instance.GetGracePeriod());
            blackOutImage.color = color;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
