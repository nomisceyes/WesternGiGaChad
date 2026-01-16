using UnityEngine;

public class BootstrapState : IState
{
    private const string InitialScene = "Initial";
    private const string Test = "Test";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly ServiceLocator _services;

    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator services)
    {
        _stateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _services = services;

        RegisterServices();
    }

    public void Enter()
    {
        _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
    }

    private void EnterLoadLevel() =>    
        _stateMachine.Enter<LoadLevelState, string>(Test);

    public void Exit()
    {       
    }

    private void RegisterServices()
    {
        _services.RegisterSingle<IInputService>(new InputService());
        _services.RegisterSingle<IAssetProvider>(new AssetProvider());
        _services.RegisterSingle<IGameFactory>(new GameFactory(ServiceLocator.Container.Single<IAssetProvider>()));

        //Game.InputService = ServiceLocator.Container.Single<IInputService>();
    }
}