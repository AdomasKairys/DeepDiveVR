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
        loopSource.loop = _isLooping;
        while (loopSource.loop)
        {
            loopSource.loop = _isLooping;
            yield return null;

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
