using System;
using System.Collections;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    private IAssetProvider _assetProvider;
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