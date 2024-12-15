using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float maxOxygen = 100f;
    [SerializeField] float gracePeriodTime = 10f;

    public static GameManager Instance { get; private set; }

    public UnityEvent OnOxygenChanged;
    public UnityEvent OnOxygenRanOut;
    public UnityEvent OnGracePeriodOver;
    public UnityEvent OnRefillOxygen;

    private float _currentOxygen = 0f;
    private bool _isOutOfOxygen = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        _currentOxygen = maxOxygen;
    }

    public float GetCurrentOxygen() => _currentOxygen;
    public float GetMaxOxygen() => maxOxygen;
    public void Resurface() {
        StopAllCoroutines();
        _isOutOfOxygen = false;
    }
    
    public void RefillOxygen()
    {
        StopAllCoroutines();
        _currentOxygen = maxOxygen;
        _isOutOfOxygen = false;
        OnRefillOxygen?.Invoke();
    }
    public void ReduceOxygen(float ammount)
    {
        if(_isOutOfOxygen) 
            return;
        _currentOxygen -= ammount;

        if (_currentOxygen <= 0.01f)
        {
            _isOutOfOxygen = true;
            OnOxygenRanOut?.Invoke();
            StartCoroutine(StartGracePeriod());
        }
        else
            OnOxygenChanged?.Invoke();
    }

    private IEnumerator StartGracePeriod()
    {
        yield return new WaitForSeconds(gracePeriodTime);
        OnGracePeriodOver?.Invoke();
    }

    public float GetGracePeriod() => gracePeriodTime;

    public static void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }   
    public static void QuitGame()
    {
        Application.Quit();
    }
    
}
