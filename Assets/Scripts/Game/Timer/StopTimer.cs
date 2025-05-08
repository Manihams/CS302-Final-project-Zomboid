using UnityEngine;

public class StopTimer : MonoBehaviour
{
    public void Stoptimer()
    {
        Timer timer = FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.StopTimer();
        }
    }
}
