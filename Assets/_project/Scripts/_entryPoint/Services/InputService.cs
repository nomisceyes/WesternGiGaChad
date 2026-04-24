using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour, IService
{
    private PlayerControls _playerControls;

    private InputAction _moveAction;
    private InputAction _aimAction;
    private InputAction _shootAction;
    private InputAction _switchWeaponAction;
    private Vector3 _currentMoveInput;

    public bool AimPressed { get; private set; }
    
    public void Init()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
        _moveAction = _playerControls.Gameplay.Move;
        _aimAction = _playerControls.Gameplay.Aim;
        _shootAction = _playerControls.Gameplay.Shoot;
        _switchWeaponAction = _playerControls.Gameplay.SwitchWeapon;
    }
    
    private void Update()
    {
        _currentMoveInput = _moveAction.ReadValue<Vector3>();
        AimPressed = _aimAction.IsPressed();
    }

    public Vector3 GetMoveInput() =>
        _currentMoveInput;

    public bool IsAiming() =>
        _aimAction.IsPressed();

    public bool Aiming() =>
        _aimAction.triggered;

    public bool IsShooting() =>
        _shootAction.triggered;

    public bool CanShoot() =>
        IsAiming() && IsShooting();

    public bool SwitchWeapon() =>
        _switchWeaponAction.triggered;
}