using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    private HealthController healthController; // Reference to the player's HealthController

    private void Awake()
    {
        healthController = GetComponent<HealthController>(); // Get the HealthController component
    }

    // Start invincibility for a given duration
    public void StartInvincibility(float invincibilityDuration)
    {
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration)); // Start the invincibility coroutine
    }

    // Coroutine to handle invincibility logic
    private IEnumerator InvincibilityCoroutine(float invincibilityDuration)
    {
        healthController.IsInvincible = true; // Set player as invincible
        yield return new WaitForSeconds(invincibilityDuration); // Wait for the invincibility duration
        healthController.IsInvincible = false; // Set player back to normal state
    }
}
