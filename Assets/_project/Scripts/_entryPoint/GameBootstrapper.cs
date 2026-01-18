using System;
using System.Collections;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    //public LoadingCurtain Curtain;
    //[SerializeField] private Transform _playerSpawnPoint;
    
    
    private IAssetProvider _assetProvider;
    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }
    
    private void Awake()
    {
        //_game = new Game(this, Curtain);

        Debug.Log("Save InputSystem");
        // _inputService = ServiceLocator.GetService<IInputService>();
        // _assetProvider = ServiceLocator.GetService<IAssetProvider>();

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //_assetProvider.Instantiate(AssetPath.HeroPath, at: _playerSpawnPoint.position);
    }

    private void Update()
    {
        _inputService?.Update();
    }

    private void OnDestroy()
    {
        if (_inputService is IDisposable disposable)
            disposable.Dispose();
    }

    public Coroutine StartRoutine(IEnumerator coroutine) =>
         StartCoroutine(coroutine);
}