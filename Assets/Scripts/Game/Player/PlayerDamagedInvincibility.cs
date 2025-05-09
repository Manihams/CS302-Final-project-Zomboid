using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Duration for which the player is invincible
    [SerializeField]
    private float _invincibilityDuration;

    private InvincibilityController _invincibilityController;

    // Get reference to the InvincibilityController component
    private void Awake()
    {
        _invincibilityController = GetComponent<InvincibilityController>();
    }

    // Call this method to start invincibility for the set duration
    public void StartInvincibility()
    {
        _invincibilityController.StartInvincibility(_invincibilityDuration);
    }
}
