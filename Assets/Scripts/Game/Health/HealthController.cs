using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth; // Current health of the player

    [SerializeField]
    private float _maxHealth;    // Maximum health of the player

    public float HealthPercentage
    {
        get{
            return _currentHealth / _maxHealth; // Get health percentage
        }
    }

    public float MaxHealth => _maxHealth; // Get maximum health value

    public bool IsInvincible { get; set; } // Flag to check if player is invincible

    public UnityEvent OnDied;        // Event triggered when player dies
    public UnityEvent OnDamaged;     // Event triggered when player takes damage
    public UnityEvent OnHealthChanged; // Event triggered when health changes

    private GameOverHandler _gameOverHandler; // Reference to GameOverHandler

    private void Start()
    {
        _gameOverHandler = FindObjectOfType<GameOverHandler>(); // Find GameOverHandler in the scene
    }

    // Method to apply damage to the player
    public void TakeDamage(float damageAmount)
    {
        if(_currentHealth == 0 || IsInvincible)  // Return if already dead or invincible
        {
            return;
        }

        _currentHealth -= damageAmount;   // Decrease health by damage amount
        OnHealthChanged.Invoke();          // Trigger health change event

        if(_currentHealth < 0) 
        {
            _currentHealth = 0; // Ensure health doesn't go below 0
        }

        if(_currentHealth <= 0)
        {
            _gameOverHandler?.ShowGameOver(); // Show game over if health reaches 0
            OnDied.Invoke(); // Trigger the death event
        }
        else
        {
            OnDamaged.Invoke(); // Trigger damage event
        }
    }

    // Method to add health to the player
    public void AddHealth(float amountToAdd)
    {
        if(_currentHealth == _maxHealth)  // Return if health is already full
        {
            return;
        }

        _currentHealth += amountToAdd;   // Increase health by the specified amount
        OnHealthChanged.Invoke();        // Trigger health change event

        if(_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;  // Ensure health doesn't exceed max health
        }
    }
}
