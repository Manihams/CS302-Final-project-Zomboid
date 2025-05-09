using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoots : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;  // Bullet prefab to instantiate when firing

    [SerializeField]
    private float _bulletSpeed; // Speed at which the bullet moves

    [SerializeField]
    private Transform _gunOffset;  // Offset position from where the bullet is fired

    [SerializeField]
    private float _timeBetweenShots;  // Time between consecutive shots

    private bool _fireContinuously;  // Whether the player is holding down fire
    private bool _fireSingle;  // Whether the player is pressing fire once
    private float _lastFireTime;   // Time of the last shot fired

    // Update is called once per frame
    void Update()
    {
        // Check if continuous or single fire is active
        if(_fireContinuously || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            // Fire if enough time has passed since last shot
            if(timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullet();

                _lastFireTime = Time.time;  // Update the time of the last fire
                _fireSingle = false;        // Reset single fire flag after shooting
            }
        }        
    }

    // Instantiate bullet and apply velocity
    private void FireBullet()
    {
        // Create a bullet at the gun offset position and rotation
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, _gunOffset.rotation);
        
        // Get Rigidbody2D of the bullet to apply movement
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        // Set the bullet's velocity
        rigidbody.linearVelocity = _bulletSpeed * _gunOffset.up;
    }

    // Handle fire input actions (continuous or single fire)
    private void OnFire(InputValue inputValue)
    {
        // Check if fire button is being held down
        _fireContinuously = inputValue.isPressed;

        // If the fire button is pressed, set single fire flag
        if(inputValue.isPressed)
        {
            _fireSingle = true;
        }
    }
}
