using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private List<BoxCollider> _spawnAreas;
    [SerializeField] private Transform _testPoint;
    [SerializeField] private Player _player;
    [SerializeField] private PopupSpawner _popupSpawner;
    [SerializeField] private int _amountEnemy;
    [SerializeField] private int _wavesAmount;
    [SerializeField] private int _increaseEnemyWaves;
    [SerializeField] private float _spawnTime;

    [SerializeField] private List<Enemy> _enemies = new();

    private WaitForSeconds _spawnDelay;
    private Vector3 _spawnOffset = new (0, 1f, 0);

    private int _currentEnemies = 0;
    private int _currentWave = 0;

    public event Action<int, int> ScoreChanged;

    private void Start()
    {
        _spawnDelay = new(_spawnTime);

        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if(_currentEnemies == _amountEnemy && _enemies.Count == 0)
        {
            Debug.Log("YOU WIN");
            _currentWave++;

            if (_currentWave < _wavesAmount)
            {
                _currentEnemies = 0;
                _amountEnemy += _increaseEnemyWaves;
                StartCoroutine(SpawnEnemy());
            }
        }
    }

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < _amountEnemy; i++)
        {
            Vector3 spawnPoint = GetRandomPointInCollider();

            Enemy enemy = Pool.Get();
            _enemies.Add(enemy);
            enemy.SetPlayerTarget(_player);
            enemy.Health.Popup += _popupSpawner.Create;

            enemy.Warp(spawnPoint + _spawnOffset);

            _currentEnemies++;

            enemy.Died += RemoveEnemy;

            ScoreChanged?.Invoke(_enemies.Count, _amountEnemy);

            yield return 0;
        }
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.Died -= RemoveEnemy;

        ScoreChanged?.Invoke(_enemies.Count, _amountEnemy);
    }

    private Vector3 GetRandomPointInCollider()
    {
        int index = UnityEngine.Random.Range(0, _spawnAreas.Count);

        Vector3 center = _spawnAreas[index].bounds.center;
        Vector3 size = _spawnAreas[index].bounds.size;

        float randomX = UnityEngine.Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = UnityEngine.Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        float randomZ = UnityEngine.Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(randomX, randomY, randomZ);
    }

    private void OnDrawGizmos()
    {
        foreach (var area in _spawnAreas)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(area.bounds.center, area.bounds.size);
        }
    }
}