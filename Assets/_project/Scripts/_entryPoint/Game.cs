using System.Linq;
using Cysharp.Threading.Tasks;
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

    public bool IsPlaying = true;
    
    private int _currentWave;
    private bool _isProcessingWave;

    private void Start()
    {
        IsPlaying = true;
        Init();
    }

    private void Init()
    {
        Global.Main = this;

        Player = FindFirstObjectByType<Player>();
        BossSpawner = FindFirstObjectByType<BossSpawner>();
        EnemySpawner = FindFirstObjectByType<EnemySpawner>();
        ResoultsHandler = FindFirstObjectByType<ResoultsHandler>();
        PanelHandler = FindFirstObjectByType<PanelHandler>();
        PopupSpawner = FindFirstObjectByType<PopupSpawner>();

        InitUIElements();

        Global.AudioManager.PlayMusic(Res.Audio.BackgroundMusic);
        
        StartWave();
    }
    
    private void InitUIElements()
    {
        var allUI = FindObjectsOfType<MonoBehaviour>()
            .Where(component => component is IUIElement)
            .Cast<IUIElement>()
            .ToArray();

        foreach (var uiElement in allUI)
        {
            uiElement.Init();
        }
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

        _ = ActiveNextWave();
    }

    private void StartWave()
    {
        //_isProcessingWave = true;
        EnemySpawner.SpawnEnemy();
    }
    
    private async UniTask ActiveNextWave()
    {
        if (_isProcessingWave) return;
        
        if (EnemySpawner.Enemies.Count == 0)
        {
            _isProcessingWave = true;
            _currentWave++;

            await ResoultsHandler.PrepareToNextWave();

            if (this != null && gameObject.activeInHierarchy)
            {
                await UniTask.Delay(2000, cancellationToken: this.GetCancellationTokenOnDestroy());
                EnemySpawner.SpawnEnemy();
            }

            _isProcessingWave = false;
        }
    }

    private async UniTask ActiveBossState()
    {
        if (_currentWave == _maxWaveAmount && _isProcessingWave == false)
        {
            _isProcessingWave = true;
            await ResoultsHandler.PrepareToBossFight();

            BossSpawner.CreateBoss();

            _isProcessingWave = false;
        }
    }
}