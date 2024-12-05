using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingSFXController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Rigidbody handRigidbody;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float audioDelay = 3f;


    float _currentDelay = 0;
    int _audioClipsLength;
    System.Random _random = new System.Random();
    void Awake()
    {
        _audioClipsLength = audioClips.Length;
    }
    void Update()
    {
        if (handRigidbody.velocity.magnitude > 1f && _currentDelay <= 0.1f)
        {
            _currentDelay = audioDelay;
            audioSource.PlayOneShot(audioClips[_random.Next(_audioClipsLength)]);
        }
        else if (_currentDelay > 0.1f)
        {
            _currentDelay -= Time.deltaTime;
        }
    }
}
