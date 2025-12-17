using UnityEngine;
using TMPro; // Add this using statement
using System;

public class GameTimer : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text timeText; // Changed from Text to TMP_Text
    
    [Header("Timer Settings")]
    [SerializeField] private bool countUp = true;
    [SerializeField] private float startTime = 0f;
    
    private float currentTime;
    private bool isTimerRunning = false;

    
    void Start()
    {
        currentTime = startTime;
        
        // If timeText is not assigned, try to find it
        if (timeText == null)
        {
            timeText = GameObject.Find("Time").GetComponent<TMP_Text>();
        }
        
        UpdateTimeDisplay();
    }
    
    void Update()
    {
        if (isTimerRunning)
        {
            if (countUp)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 0)
                {
                    currentTime = 0;
                    isTimerRunning = false;
                }
            }
            
            UpdateTimeDisplay();
        }
    }
    
    void UpdateTimeDisplay()
    {
        if (timeText != null)
        {
            // Simple format: seconds with one decimal
            timeText.text = "Time: " + currentTime.ToString("F1"); // "Time: 12.3"
        }
    }
    
    public void StartTimer() => isTimerRunning = true;
    public void StopTimer() => isTimerRunning = false;
    public void ResetTimer()
    {
        currentTime = startTime;
        UpdateTimeDisplay();
    }
    public float GetCurrentTime() => currentTime;
}