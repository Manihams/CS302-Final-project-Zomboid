using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoots : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    private Transform _gunOffset;

    [SerializeField]
    private float _timeBetweenShots;

    private bool _fireContinuously;
    private bool _fireSingle;
    private float _lastFireTime;


    // Update is called once per frame
    void Update()
    {
        if(_fireContinuously || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;
            if(timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullet();
                
                _lastFireTime = Time.time;
                _fireSingle = false;
            }
        }        
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _gunOffset.position, _gunOffset.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.linearVelocity = _bulletSpeed * _gunOffset.up;
    }

    private void OnFire(InputValue inputValue)
    {
        
        _fireContinuously = inputValue.isPressed;

        if(inputValue.isPressed)
        {
            _fireSingle = true;
        }
    }
}
