using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints; 
    [SerializeField] private float _minSpawnTime = 2f;
    [SerializeField] private float _maxSpawnTime = 5f;
    [SerializeField] private GameObject _player;

    private float _timeUntilSpawn;
    private List<GameObject> _activeEnemies = new List<GameObject>(); // Tracks active zombies
    private Queue<GameObject> _enemyPool = new Queue<GameObject>(); // Recycles defeated zombies
    private int _waveNumber = 1;  
    private int _enemiesPerWave = 5;  
    private bool _waveInProgress = false;

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

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

    private void SpawnEnemy()
    {
        if (_spawnPoints.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, _spawnPoints.Length);
        Transform spawnPoint = _spawnPoints[randomIndex];

        GameObject enemy;

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

        _activeEnemies.Add(enemy);
    }


    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minSpawnTime, _maxSpawnTime);
    }

    private void CleanupDestroyedEnemies()
    {
        _activeEnemies.RemoveAll(enemy => enemy == null);
    }

    public void EnemyKilled(GameObject enemy)
    {
        if (_activeEnemies.Contains(enemy))
        {
            _activeEnemies.Remove(enemy);
            _enemyPool.Enqueue(enemy); // Store defeated enemy for reuse
            enemy.SetActive(false);
        }

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
        yield return new WaitForSeconds(3f);  

        _waveNumber++;
        _enemiesPerWave += 5;  
        _minSpawnTime = Mathf.Max(0.5f, _minSpawnTime - 0.1f);  
        _maxSpawnTime = Mathf.Max(1f, _maxSpawnTime - 0.2f);

        Debug.Log("Wave " + _waveNumber + " starting! Zombies: " + _enemiesPerWave);
        _waveInProgress = false;
    }
}


