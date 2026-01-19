using UnityEngine;

public class Mover : IUpdateListener
{
    private readonly PlayerMoverStats _playerMoverStats;
    private readonly CharacterController _characterController;
    private readonly Transform _camera;
    private readonly Transform _yawTarget;
    private readonly Transform _transform;
    private readonly IInputService _inputService;
    
    private Vector2 _moveInput;
    private float _currentSpeed;

    public float CurrentSpeed => _moveInput.sqrMagnitude;

    public Mover(CharacterController characterController, Transform camera, Transform yawTarget,
        PlayerMoverStats playerMoverStats, IInputService inputService, Transform transform)
    {
        _characterController = characterController;
        _camera = camera;
        _yawTarget = yawTarget;
        _playerMoverStats = playerMoverStats;
        _inputService = inputService;
        _transform = transform;
    }

    public void Update() =>
        Move();

    private void Move()
    {
        _moveInput = _inputService.GetMoveInput();
        Vector3 moveDirection = Vector3.zero;
        float targetSpeed = moveDirection.z < 0 ? _playerMoverStats.BackwardSpeed : _playerMoverStats.Speed;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, _playerMoverStats.Acceleration * Time.deltaTime);

        moveDirection.y -= _playerMoverStats.Gravity * Time.deltaTime;

        if (_inputService.AimPressed)
        {
            Vector3 forward = _transform.forward;
            Vector3 right = _transform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            moveDirection = forward * _moveInput.y + right * _moveInput.x;
        }
        else
        {
            Vector3 forward = _camera.forward;
            Vector3 right = _camera.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            moveDirection = forward * _moveInput.y + right * _moveInput.x;
        }

        _characterController.Move(_currentSpeed * Time.deltaTime * moveDirection);

        if (_inputService.AimPressed)
        {
            Vector3 lookDirection = _yawTarget.forward;
            lookDirection.y = 0f;

            if (lookDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        else if (_playerMoverStats.ShouldFaceMoveDirection && moveDirection.sqrMagnitude > 0.001f)
        {
            RotateTowardsMovement(moveDirection);
        }
    }

    private void RotateTowardsMovement(Vector3 moveDirection)
    {
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, toRotation,
            _playerMoverStats.RotationSpeed * Time.deltaTime);
    }
}