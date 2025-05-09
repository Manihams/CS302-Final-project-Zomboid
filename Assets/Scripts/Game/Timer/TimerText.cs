using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float startTime;               // Time when the timer starts
    private bool isRunning = true;         // Flag to check if the timer is running
    public TextMeshProUGUI timerText;      // Reference to the TextMeshProUGUI component to display time

    void Start()
    {
        startTime = Time.time;             // Set start time to the current time
        isRunning = true;                   // Ensure timer starts running
    }

    void Update()
    {
        if (!isRunning) return;            // If timer is stopped, exit the update

        float t = Time.time - startTime;   // Calculate the elapsed time
        string minutes = ((int)t / 60).ToString();  // Get the minutes part of the time
        string seconds = (t % 60).ToString("f2");  // Get the seconds part, formatted to 2 decimal places
        timerText.text = minutes + ":" + seconds;    // Display the formatted time on the UI
    }

    // Stop the timer when called
    public void StopTimer()
    {
        isRunning = false;                  // Setting the flag to false to stop the timer
    }
}
