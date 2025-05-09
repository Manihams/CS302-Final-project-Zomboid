using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Stores enemy speed
    [SerializeField]
    private float _speed;

    // Stores the rotation speed towards enemy
    [SerializeField]
    private float _rotationSpeed;

    // checks if the zombie is at the edge of the screen
    [SerializeField]
    private float _screenBorder;

    private Rigidbody2D _rigidbody;
    private PlayerAwareness _playerAwareness;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;
    private Camera _camera;

    private void Awake()
    {
        // Initialized all the components and variables
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwareness = GetComponent<PlayerAwareness>();
        _targetDirection = transform.up;
        _camera = Camera.main;
    }

    // Update is called once per frame
    // tracks enemy's movement
    // Rotate the face towards the player
    // moves at constant velocity
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    // Adjusts the direction, tracks player, and adjust movement near screen edges
    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
        HandleEnemyOffScreen();
    }

    // Controls the zombie directions and postion them at random places
    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;
        if(_changeDirectionCooldown <= 0)
        {
            float angleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
            _targetDirection = rotation * _targetDirection;

            _changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    // If the enemy detects the player, changes the direction towards the player
    private void HandlePlayerTargeting()
    {
        if(_playerAwareness.AwareOfPlayer)
        {
            _targetDirection = _playerAwareness.DirectionToPlayer;
        }
    }

    private void HandleEnemyOffScreen()
    {
        // Converts the world position to screen coordinates
         Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        // Changes its horizontal direction if it's near the screen edge
        if((screenPosition.x < _screenBorder && _targetDirection.x < 0) || (screenPosition.x > _camera.pixelWidth - _screenBorder && _targetDirection.x > 0))
        {
            _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
        }
        
        // Changes its vertical direction if it's near the screen edge
        if((screenPosition.y < _screenBorder && _targetDirection.y < 0) || (screenPosition.y > _camera.pixelHeight - _screenBorder && _targetDirection.y > 0))
        {
            _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
        }
    }

    // Calculates the target rotation and moves toward the target at a set speed
    private void RotateTowardsTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);
    }

    // Moves the enemy forward at a set speed
    private void SetVelocity()
    {
        _rigidbody.linearVelocity = transform.up * _speed;
    }
}
