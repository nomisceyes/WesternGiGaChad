using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : IInputService, IDisposable
{
    private readonly PlayerControls _playerControls;

    private InputAction _moveAction;
    private InputAction _aimAction;
    private InputAction _shootAction;
    private Vector2 _currentMoveInput;
    
    public bool AimPressed { get; private set; }

    public InputService()
    {
        _playerControls = new PlayerControls();

        Enable();
    }

    public void Enable()
    {
        _playerControls.Enable();
        _moveAction = _playerControls.Gameplay.Move;
        _aimAction = _playerControls.Gameplay.Aim;
        _shootAction = _playerControls.Gameplay.Shoot;
    }

    public void Update()
    {
        _currentMoveInput = _moveAction.ReadValue<Vector2>();
        AimPressed = _aimAction.IsPressed();
    }

    public Vector2 GetMoveInput() =>
        _currentMoveInput;

    public bool IsAiming() =>
        _aimAction.IsPressed();

    public bool IsShooting() =>
        _shootAction.triggered;

    public void Dispose()
    {
        Debug.Log("Input Dispose");
        _playerControls.Disable();
    }
}