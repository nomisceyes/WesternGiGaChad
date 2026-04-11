using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BossSpawner _bossSpawner;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private Player _player;
    [SerializeField] private ResoultsHandler _resoultsHandler;
    [SerializeField] private PanelHandler _panelHandler;

    [SerializeField] private int _maxWaveAmount;
    
    private int _currentWave;
    private bool _prepareToNextWave = false;
    private bool _bossFight = false;

    public bool IsPlaying = true;
    
    private void Awake()
    {
        _bossSpawner.SetTarget(_player);
        IsPlaying = true;
        
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (IsPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            _panelHandler.PauseGame();
            IsPlaying = false;
        }
        else if (IsPlaying == false && Input.GetKeyDown(KeyCode.Escape))
        {
            _panelHandler.StartGame();
            IsPlaying = true;
        }
        
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