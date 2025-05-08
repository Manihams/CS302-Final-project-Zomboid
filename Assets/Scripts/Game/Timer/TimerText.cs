using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float startTime;
    private bool isRunning = true;
    public TextMeshProUGUI timerText;

    void Start()
    {
        startTime = Time.time;
        isRunning = true;
    }

    void Update()
    {
        if (!isRunning) return;

        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}



