using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerControls _playerControls;

    public InputAction AimAction;
    public InputAction ShootAction;

    private void Start()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        AimAction = _playerControls.Gameplay.Aim;
        ShootAction = _playerControls.Gameplay.Shoot;
    }

    public bool IsAiming() =>    
        AimAction.IsPressed();  

    public bool IsShooting() =>
        ShootAction.triggered;
}