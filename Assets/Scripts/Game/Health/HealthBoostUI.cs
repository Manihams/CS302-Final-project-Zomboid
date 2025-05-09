using UnityEngine;
using TMPro;
using System.Collections;

public class HealthBoostUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI boostText;    // Reference to the TextMeshProUGUI component for boost display
    [SerializeField] private float displayTime = 1.5f;      // Time for which the boost message is displayed

    private void Start()
    {
        // Initially hide the boost text
        boostText.gameObject.SetActive(false);
    }

    // Show the health boost amount on screen
    public void ShowHealthBoost(int amount)
    {
        boostText.text = $"+{amount} HP!";           // Set boost text
        boostText.gameObject.SetActive(true);        // Show the boost text
        StartCoroutine(HideAfterDelay());            // Start coroutine to hide text after a delay
    }

    // Coroutine to hide the boost text after a delay
    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime); // Wait for displayTime seconds
        boostText.gameObject.SetActive(false);        // Hide the boost text
    }
}
