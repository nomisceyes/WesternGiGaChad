using UnityEditor;
using UnityEngine;

public class LoadLevelState : IPayloadedState<string>
{  
    private const string InitPointTag = "PlayerInitPoint";

    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _curtain;
    private readonly IGameFactory _gameFactory;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain curtain, IGameFactory gameFactory)
    {
        _gameStateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _curtain = curtain;
        _gameFactory = gameFactory;
    }

    public void Enter(string sceneName)
    {
        _curtain.Show();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit()
    {       
        _curtain.Hide();
    }

    private void OnLoaded()
    {
        var initialPoint = GameObject.FindWithTag(InitPointTag);

        _gameFactory.CreatePlayer(at: initialPoint);
        
        _gameStateMachine.Enter<GameLoopState>();
    } 
}