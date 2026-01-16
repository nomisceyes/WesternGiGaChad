using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawner : Spawner<Enemy>
{
    private readonly List<Enemy> _enemies = new();

    [SerializeField] private TextMeshProUGUI _victoryText;
    [SerializeField] private TextMeshProUGUI _prepareText;


    [SerializeField] private List<BoxCollider> _spawnAreas;
    [SerializeField] private Transform _testPoint;
    [SerializeField] private Player _player;
    [SerializeField] private PopupSpawner _popupSpawner;
    [SerializeField] private int _maxAmountEnemy;
    [SerializeField] private int _wavesAmount;
    [SerializeField] private int _increaseEnemyWaves;

    private int _currentEnemies = 0;
    private int _currentWave = 0;
    private bool _prerareToNextWave = false;

    public event Action<int, int> ScoreChanged;

    private void Start() =>
        StartCoroutine(SpawnEnemy());

    private void Update()
    {
        if (_currentEnemies == _maxAmountEnemy && _enemies.Count == 0)
        {
            if (_prerareToNextWave == false)
                StartCoroutine(PrepareToNextWaveRoutine());
        }
    }

    private IEnumerator PrepareToNextWaveRoutine()
    {
        _victoryText.gameObject.SetActive(true);
        _prerareToNextWave = true;

        yield return new WaitForSeconds(3f);

        _victoryText.gameObject.SetActive(false);       

        yield return new WaitForSeconds(1f);

        _prepareText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _prepareText.gameObject.SetActive(false);

        Debug.Log("3");

        yield return new WaitForSeconds(1f);

        Debug.Log("2");

        yield return new WaitForSeconds(1f);

        Debug.Log("1");

        yield return new WaitForSeconds(0.5f);
       
        _currentWave++;

        if (_currentWave < _wavesAmount)
        {
            _currentEnemies = 0;
            _maxAmountEnemy += _increaseEnemyWaves;

            StartCoroutine(SpawnEnemy());
        }

        _prerareToNextWave = false;
    }

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < _maxAmountEnemy; i++)
        {
            Enemy enemy = Pool.Get();

            enemy.SetStartPosition(_testPoint.position);

            //enemy.SetStartPosition(GetRandomPointInCollider());           
            enemy.SetPlayerTarget(_player);
            enemy.Health.Popup += _popupSpawner.Create;
            enemy.Health.Restore();

            _enemies.Add(enemy);
            _currentEnemies++;

            enemy.Died += RemoveEnemy;

            ScoreChanged?.Invoke(_enemies.Count, _maxAmountEnemy);

            yield return 0;
        }
    }

    private void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        enemy.Health.Popup -= _popupSpawner.Create;
        enemy.Died -= RemoveEnemy;

        ScoreChanged?.Invoke(_enemies.Count, _maxAmountEnemy);
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