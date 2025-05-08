using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maxHealth;

    public float HealthPercentage
    {
        get{
            return _currentHealth / _maxHealth;
        }
    }

    public float MaxHealth => _maxHealth;

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied;

    public UnityEvent OnDamaged;

    public UnityEvent OnHealthChanged;

    private GameOverHandler _gameOverHandler;

    private void Start()
    {
        _gameOverHandler = FindObjectOfType<GameOverHandler>();
    }

    public void TakeDamage(float damageAmount)
    {
        if(_currentHealth == 0)
        {
            return;
        }

        if(IsInvincible)
        {
            return;
        }

        _currentHealth -= damageAmount;

        OnHealthChanged.Invoke();
        
        if(_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if(_currentHealth <= 0)
        {
            _gameOverHandler?.ShowGameOver();
            OnDied.Invoke();
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if(_currentHealth == _maxHealth)
        {
            return;
        }

        _currentHealth += amountToAdd;

        OnHealthChanged.Invoke();

        if(_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}
