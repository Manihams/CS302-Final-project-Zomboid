using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForeGroundImage;  // Reference to the health bar foreground image

    // Update the health bar UI based on the player's current health percentage
    public void UpdateHealthBar(HealthController healthController)
    {
        // Set the fill amount of the health bar based on the health percentage
        _healthBarForeGroundImage.fillAmount = healthController.HealthPercentage;
    }
}
