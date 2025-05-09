using UnityEngine;

public class StopTimer : MonoBehaviour
{
    // This method will stop the timer when it's called
    public void Stoptimer()
    {
        // This function will find the instance of the timer class
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.StopTimer();
        }
    }
}
