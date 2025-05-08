using UnityEngine;
using TMPro;
using System.Collections;

public class HealthBoostUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI boostText;
    [SerializeField] private float displayTime = 1.5f;

    private void Start()
    {
        boostText.gameObject.SetActive(false);
    }

    public void ShowHealthBoost(int amount)
    {
        boostText.text = $"+{amount} HP!";
        boostText.gameObject.SetActive(true);
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        boostText.gameObject.SetActive(false);
    }
}

