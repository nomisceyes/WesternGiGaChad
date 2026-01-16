using System;
using System.Collections.Generic;

public class GameStateMachine
{
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _currentState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, ServiceLocator services)
    {
        _states = new Dictionary<Type, IExitableState>()
        {
            [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
            [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, curtain, ServiceLocator.Container.Single<IGameFactory>()),
            [typeof(GameLoopState)] = new GameLoopState(this),
        };
    }

    public void Enter<TState>() where TState : class, IState
    {
        IState state = ChangeState<TState>();
        state?.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
        TState state = ChangeState<TState>();
        state?.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
        _currentState?.Exit();

        TState state = GetState<TState>();
        _currentState = state;

        return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
        _states[typeof(TState)] as TState;
}