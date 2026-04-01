using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BossSpawner _bossSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Player _player;
    [SerializeField] private ResoultsHandler _resoultsHandler;

    [SerializeField] private int _maxWaveAmount;
    
    private int _currentWave;
    private bool _prepareToNextWave = false;
    private bool _bossFight = false;

    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }
    
    private void Awake()
    {
        _bossSpawner.SetTarget(_player);
        
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        _inputService.Update();

        SwitchGameState();  
    }

    private void SwitchGameState()
    {
        if (_currentWave == _maxWaveAmount)
        {
            if (_bossFight == false)
            {
                _bossFight = true;
                  
                _resoultsHandler.BossFight();
                _bossSpawner.CreateBoss();
            }
        }
        else if (_enemySpawner.CurrentEnemies == _enemySpawner.MaxAmountEnemy && _enemySpawner.Enemies.Count == 0)
        {
            if (_prepareToNextWave == false)
                ActiveNextWave();
        }
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

        _prepareToNextWave = false;
    }
}