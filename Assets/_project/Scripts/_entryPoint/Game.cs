using System.Collections;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Boss _boss;
    [SerializeField] private Transform _bossSpawn;
    [SerializeField] private Player _player;
    [SerializeField] private ResoultsHandler _resoultsHandler;

    [SerializeField] private int _maxWaveAmount;

    private int _currentWave;
    private bool _prerareToNextWave = false;
    private bool _bossFight = false;

    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        _inputService.Update();

        if (_currentWave == _maxWaveAmount)
        {
            if (_bossFight == false)
            {
                _bossFight = true;
                CreateBoss();
            }
        }
        else if (_enemySpawner.CurrentEnemies == _enemySpawner.MaxAmountEnemy && _enemySpawner.Enemies.Count == 0)
        {
            if (_prerareToNextWave == false)
                ActiveNextWave();
        }
    }

    private void CreateBoss()
    {
        _resoultsHandler.BossFight();

        Boss boss = Instantiate(_boss, _bossSpawn.position, Quaternion.identity);
        boss.SetPlayerTarget(_player);
    }


    private void ActiveNextWave()
    {
        _resoultsHandler.PrepareToNextWave();

        _currentWave++;

        if (_currentWave < _maxWaveAmount)
        {
            _enemySpawner.CurrentEnemies = 0;
            _enemySpawner.MaxAmountEnemy += 0;

            _enemySpawner.Spawn();
        }

        _prerareToNextWave = false;
    }
}