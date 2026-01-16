using System;
using System.Collections;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
    public LoadingCurtain Curtain;

    private Game _game;
    private IInputService _inputService;

    private void Awake()
    {
        _game = new(this, Curtain);
        _game.StateMachine.Enter<BootstrapState>();

        Debug.Log("Save InputSystem");
        _inputService = ServiceLocator.Container.Single<IInputService>();

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