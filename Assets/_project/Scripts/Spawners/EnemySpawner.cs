using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : Spawner<Enemy>
{
    public List<Enemy> Enemies = new List<Enemy>();
    public int CurrentEnemies = 0;
    public int MaxAmountEnemy;

    [SerializeField] private List<BoxCollider> _spawnAreas;
    [SerializeField] private Transform _testPoint;
    [SerializeField] private Player _player;
    [SerializeField] private PopupSpawner _popupSpawner;

    public event Action<int, int> ScoreChanged;

    private void Start() =>
        StartCoroutine(SpawnEnemy());
    
    public void Spawn() =>
        StartCoroutine(SpawnEnemy());
    
    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < MaxAmountEnemy; i++)
        {
            Enemy enemy = Pool.Get();

            enemy.SetStartPosition(GetRandomPointInCollider());           
            enemy.SetPlayerTarget(_player);
            enemy.Health.Popup += _popupSpawner.Create;
            enemy.Health.Reset();

            Enemies.Add(enemy);
            CurrentEnemies++;

            enemy.Died += RemoveEnemy;

            ScoreChanged?.Invoke(Enemies.Count, MaxAmountEnemy);

            yield return 0;
        }
    }

    private void RemoveEnemy(Enemy enemy)
    {
        Enemies.Remove(enemy);
        enemy.Health.Popup -= _popupSpawner.Create;
        enemy.Died -= RemoveEnemy;

        ScoreChanged?.Invoke(Enemies.Count, MaxAmountEnemy);
    }

    private Vector3 GetRandomPointInCollider()
    {
        int index = Random.Range(0, _spawnAreas.Count);

        Vector3 center = _spawnAreas[index].bounds.center;
        Vector3 size = _spawnAreas[index].bounds.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomY = Random.Range(center.y - size.y / 2, center.y + size.y / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

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