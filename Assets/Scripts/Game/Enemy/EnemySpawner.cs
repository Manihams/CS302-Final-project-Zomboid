using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Prefabs for spawning enemies and array of spawn points where zombies appear
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints; 
    // Min and Max time intervals for spawning enemies
    [SerializeField] private float _minSpawnTime = 2f;
    [SerializeField] private float _maxSpawnTime = 5f;
    [SerializeField] private GameObject _player;

    private float _timeUntilSpawn;
    // Lists all the active enemies in the scene
    private List<GameObject> _activeEnemies = new List<GameObject>(); 
    // Stores all the defeated enemies for reuse 
    private Queue<GameObject> _enemyPool = new Queue<GameObject>(); 
    private int _waveNumber = 1;  
    private int _enemiesPerWave = 5;  
    private bool _waveInProgress = false;

    // Starts the first wave when the game begins
    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    // Updates and Spawns the enemies by calculating the time and # of enemies active on the screen
    private void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;

        if (_timeUntilSpawn <= 0 && _activeEnemies.Count < _enemiesPerWave)
        {
            SpawnEnemy();
            SetTimeUntilSpawn();
        }

        CleanupDestroyedEnemies();
    }

    // Checks if there are enough spawn points
    private void SpawnEnemy()
    {
        if (_spawnPoints.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, _spawnPoints.Length);
        Transform spawnPoint = _spawnPoints[randomIndex];

        GameObject enemy;

        // Checks if it has enough enemies to respawn
        if (_enemyPool.Count > 0)
        {   
            enemy = _enemyPool.Dequeue();
            enemy.transform.position = spawnPoint.position;
            enemy.SetActive(true);
        }
        else
        {
            enemy = Instantiate(_enemyPrefab, spawnPoint.position, Quaternion.identity);
        }

        // Adds the spawned enemy to the active enemies list
        _activeEnemies.Add(enemy);
    }


    // This will set the next spawn time randomly 
    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minSpawnTime, _maxSpawnTime);
    }

    // removes all null references to keep the list clean
    private void CleanupDestroyedEnemies()
    {
        _activeEnemies.RemoveAll(enemy => enemy == null);
    }

    public void EnemyKilled(GameObject enemy)
    {    
        // Checks if the enemy is in the active list, removes it and stores it to reuse
        if (_activeEnemies.Contains(enemy))
        {
            _activeEnemies.Remove(enemy);
            _enemyPool.Enqueue(enemy); // Store defeated enemy for reuse
            enemy.SetActive(false);
        }

        // Updates the score when the enemy is killed
        EnemiesKilled.Instance?.AddScore(1);

        // Start new wave when all zombies are gone
        if (_activeEnemies.Count == 0 && !_waveInProgress)
        {
            StartCoroutine(StartNextWave());
        }
    }

    private IEnumerator StartNextWave()
    {
        _waveInProgress = true;
        // Waits 3 seconds before starting a wave
        yield return new WaitForSeconds(3f);  

        // Increases the wave number and enemy count to make it difficult
        _waveNumber++;
        _enemiesPerWave += 5;  
        // Decrease the spawn intervals to increase difficulty
        _minSpawnTime = Mathf.Max(0.5f, _minSpawnTime - 0.1f);  
        _maxSpawnTime = Mathf.Max(1f, _maxSpawnTime - 0.2f);

        // Just checks if the wave system is working or not
        Debug.Log("Wave " + _waveNumber + " starting! Zombies: " + _enemiesPerWave);
        _waveInProgress = false;
    }
}


