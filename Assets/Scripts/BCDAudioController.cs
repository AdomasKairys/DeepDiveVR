using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCDAudioController : MonoBehaviour
{
    [SerializeField] AudioSource loopSource;
    [SerializeField] AudioSource startSource;
    [SerializeField] AudioSource endSource;

    bool _isPlaying = false;
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
        _isPlaying = true;
        while (_isPlaying)
        {
            yield return null;
            if (startSource.isPlaying)
                continue;

            loopSource.loop = _isLooping;
            if (loopSource.isPlaying && loopSource.loop)
                continue;
            else if (loopSource.loop)
            {
                loopSource.Play();
                continue;
            }
            loopSource.Stop();
            _isPlaying = false;
        }
        endSource.Play();
    }
}
