using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    private float timer = 0.0f;
    private bool isTimerRunning = false;

    private void Start()
    {
        DisplayTime();
    }
    void Update()
    {
        //if (isTimerRunning)
        timer += Time.deltaTime;
        DisplayTime();
    }
    void DisplayTime()
    {
        int timerInSeconds = Mathf.RoundToInt(timer);
        int minutes = Mathf.FloorToInt(timerInSeconds / 60);
        int seconds = Mathf.FloorToInt(timerInSeconds % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void StartTimer() => isTimerRunning = true;
    public void StopTimer() => isTimerRunning = false;
    public void ReseTimer() => timer = 0.0f;
}
