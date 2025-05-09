using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private int _zombiesToKillForBoost = 5;   // Number of zombies to kill for a health boost
    [SerializeField] private HealthBoostUI healthBoostUI;  // UI element to show health boost (assign in Inspector)

    private HealthController _healthController;   // Reference to the player's health controller
    private int _killCount;    // Tracks the number of zombies killed

    private void Awake()
    {
        // Get the HealthController component from the player
        _healthController = GetComponent<HealthController>();
        
        // Check if HealthController is missing
        if (_healthController == null)
        {
            Debug.LogError("HealthController not found on the Player GameObject!");
        }

        // Find and assign the HealthBoostUI component if not already assigned
        if (healthBoostUI == null)
        {
            healthBoostUI = FindObjectOfType<HealthBoostUI>();
        }
    }

    public void RegisterKill()
    {
        _killCount++; // Increment the kill count

        // Check if the player has killed enough zombies for a health boost
        if (_killCount % _zombiesToKillForBoost == 0 && _healthController != null)
        {
            // Increase health by 5 points
            _healthController.AddHealth(5); 

            // Show the health boost UI if available
            if (healthBoostUI != null)
            {
                healthBoostUI.ShowHealthBoost(5); // Show health boost effect in UI
            }

            Debug.Log("Player powered up! Health increased by 5.");
        }
    }
}
