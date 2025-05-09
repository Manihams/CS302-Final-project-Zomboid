using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // amount of damage the enemy does when collides with the player
    [SerializeField]
    private float _damageAmount;
        // This function is called when the enemy contact another 2D collider
       private void OnCollisionStay2D(Collision2D collision)
       {
            // Checks if it collides with the player
            if(collision.gameObject.GetComponent<PlayerMovement>())
            {
                // Health bar is updated when the enemy attacks the player
                var healthController = collision.gameObject.GetComponent<HealthController>();

                // Player's health changes when the zombie attacks
                healthController.TakeDamage(_damageAmount);
            }
        }
}
