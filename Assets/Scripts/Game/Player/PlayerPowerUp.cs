using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private int _zombiesToKillForBoost = 5;
    [SerializeField] private HealthBoostUI healthBoostUI; // Assign this in Inspector

    private HealthController _healthController;
    private int _killCount;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
        if (_healthController == null)
        {
            Debug.LogError("HealthController not found on the Player GameObject!");
        }

        
        if (healthBoostUI == null)
        {
            healthBoostUI = FindObjectOfType<HealthBoostUI>();
        }
    }

    public void RegisterKill()
    {
        _killCount++;

        if (_killCount % _zombiesToKillForBoost == 0 && _healthController != null)
        {
            _healthController.AddHealth(5); // Always increase health

            if (healthBoostUI != null)
            {
                healthBoostUI.ShowHealthBoost(5); // Show UI boost effect
            }

            Debug.Log("Player powered up! Health increased by 5.");
        }
    }

}



