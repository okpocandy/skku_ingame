using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefabs;
    public List<Transform> spawnPointList;
    private float _spawnTimer = 0f;
    [SerializeField]
    private float _spawnInterval = 3f;

    [SerializeField]
    private int _currentEnemyCount = 0;
    private int _maxEnemyCount = 10;

    private void Update()
    {
        if (_currentEnemyCount >= _maxEnemyCount)
            return;

        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnInterval)
        {
            _spawnTimer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Transform spawnPoint = spawnPointList[Random.Range(0, spawnPointList.Count)];
        var enemy = Instantiate(enemyPrefabs, spawnPoint.position, Quaternion.identity);
        _currentEnemyCount++;
    }
}
