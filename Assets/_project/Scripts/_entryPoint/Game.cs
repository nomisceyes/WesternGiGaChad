using UnityEngine;

public class Game : MonoBehaviour
{
    [HideInInspector] public BossSpawner BossSpawner;
    [HideInInspector] public EnemySpawner EnemySpawner;
    [HideInInspector] public Player Player;
    [HideInInspector] public ResoultsHandler ResoultsHandler;
    [HideInInspector] public PanelHandler PanelHandler;
    [HideInInspector] public PopupSpawner PopupSpawner;

    [SerializeField] private int _maxWaveAmount;

    private int _currentWave;
    private bool _prepareToNextWave = false;
    private bool _bossFight = false;

    public bool IsPlaying = true;

    private void Start()
    {
        IsPlaying = true;
        Init();
    }

    private void Init()
    {
        Player = FindFirstObjectByType<Player>();
        BossSpawner = FindFirstObjectByType<BossSpawner>();
        EnemySpawner = FindFirstObjectByType<EnemySpawner>();
        ResoultsHandler = FindFirstObjectByType<ResoultsHandler>();
        PanelHandler = FindFirstObjectByType<PanelHandler>();
        PopupSpawner = FindFirstObjectByType<PopupSpawner>();

        ServiceLocator.AudioManager.PlayMusic(Res.Audio.BackgroundMusic);
        
        EnemySpawner.Spawn();
    }

    private void Update()
    {
        if (IsPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            PanelHandler.PauseGame();
            IsPlaying = false;
        }
        else if (IsPlaying == false && Input.GetKeyDown(KeyCode.Escape))
        {
            PanelHandler.StartGame();
            IsPlaying = true;
        }

        SwitchGameState();
    }

    private void SwitchGameState()
    {
        // if (_currentWave == _maxWaveAmount)
        // {
        //     if (_bossFight == false)
        //     {
        //         _bossFight = true;
        //
        //         ResoultsHandler.BossFight();
        //         BossSpawner.CreateBoss();
        //     }
        // }
        // else if (EnemySpawner.CurrentEnemies == EnemySpawner.MaxAmountEnemy && EnemySpawner.Enemies.Count == 0)
        // {
        //     if (_prepareToNextWave == false)
        //         ActiveNextWave();
        // }
    }

    private void ActiveNextWave()
    {
        ResoultsHandler.PrepareToNextWave();

        _currentWave++;

        if (_currentWave < _maxWaveAmount)
        {
            EnemySpawner.CurrentEnemies = 0;
            EnemySpawner.MaxAmountEnemy += 0;

            EnemySpawner.Spawn();
        }

        _prepareToNextWave = false;
    }
}