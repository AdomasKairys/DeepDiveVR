using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwimmingSFXController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] InputActionReference controllerVelocity;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float minVelocity = 1f;
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
        var handVelocity = controllerVelocity.action.ReadValue<Vector3>();
        if (handVelocity.magnitude > minVelocity && _currentDelay <= 0.1f)
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
