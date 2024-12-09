using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCDAudioController : MonoBehaviour
{
    [SerializeField] AudioSource loopSource;
    [SerializeField] AudioSource startSource;
    [SerializeField] AudioSource endSource;

    bool _isLooping = true;

    public void StopAudio()
    {
        _isLooping = false;
    }
    public void PlayAudio()
    {
        _isLooping = true;
        StartCoroutine(PlayAudioCoroutine());
    }
    private IEnumerator PlayAudioCoroutine()
    {
        startSource.Play();
        while (loopSource.loop)
        {
            yield return null;
            loopSource.loop = _isLooping;

            if (startSource.isPlaying)
                continue;

            if (loopSource.isPlaying && loopSource.loop)
                continue;
            else if (loopSource.loop)
            {
                loopSource.Play();
                continue;
            }
        }
        startSource.Stop();
        loopSource.Stop();
        endSource.Play();
    }
}
