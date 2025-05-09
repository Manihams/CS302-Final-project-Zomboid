using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{
    // To let enemy know if it is aware of player 
    public bool AwareOfPlayer { get; private set; }

    // Stores the direction from the enemy to the player as unit vector
    public Vector2 DirectionToPlayer { get; private set; }

    // Max distance for enemies to detect the player
    [SerializeField]
    private float _playerAwarenessDistance;

    private Transform _player;

    // Finds the player's transform by locating the player movement by calling the script 
    private void Awake(){
        _player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculates the vector from the enemy to the player
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;

        // Checks if the player is close to the enemy
        if(enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
        {
            AwareOfPlayer = true;
        }
        else{
            AwareOfPlayer = false;
        }
    }
}
