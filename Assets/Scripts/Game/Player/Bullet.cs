using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;

    // Get reference to the main camera
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        // Destroy bullet if it goes off screen
        DestroyWhenOffScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if bullet hits an enemy
        if (collision.GetComponent<EnemyMovement>())
        {
            // Destroy the enemy and the bullet
            Destroy(collision.gameObject);
            Destroy(gameObject);

            // Add to score if score tracker exists
            if (EnemiesKilled.Instance != null)
            {
                EnemiesKilled.Instance.AddScore(1);
            }
        }
    }

    private void DestroyWhenOffScreen()
    {
        // Convert bullet world position to screen position
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        // Destroy bullet if it's outside the screen bounds
        if(screenPosition.x < 0 || screenPosition.x > _camera.pixelWidth || screenPosition.y < 0 || screenPosition.y > _camera.pixelHeight)
        {
            Destroy(gameObject);
        }
    }
}
