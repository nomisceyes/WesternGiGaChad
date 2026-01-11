using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    [SerializeField] private Player _player;

    [SerializeField] private Transform _testPoint;

    [SerializeField] private List<BoxCollider> _spawnAreas;
    [SerializeField] private PopupSpawner _popupSpawner;

    private List<Enemy> _enemies = new();

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        Vector3 spawnPoint = GetRandomPointInCollider();

        Enemy enemy = Pool.Get();
        _enemies.Add(enemy);
        enemy.SetPlayerTarget(_player);
        enemy.Health.Popup += _popupSpawner.Create;
        enemy.transform.position = _testPoint.position;

        //enemy.transform.position = spawnPoint + new Vector3(0, 1f, 0);

        yield return new WaitForSeconds(2f);
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