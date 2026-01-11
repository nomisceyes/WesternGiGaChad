using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [SerializeField] private InputActionReference _moveInput;

    private PlayerControls _playerControls;
    private InputAction AimAction;
    private InputAction ShootAction;

    public bool AimPressed { get; private set; }

    private void Start()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        AimAction = _playerControls.Gameplay.Aim;
        ShootAction = _playerControls.Gameplay.Shoot;
    }

    private void Update()
    {
        AimPressed = IsAiming();
    }

    public bool IsAiming() =>
        AimAction.IsPressed();

    public bool IsShooting() =>
        ShootAction.triggered;
}