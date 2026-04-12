using UnityEngine;

public class Mover : MonoBehaviour
{
    private const float Gravity = 9.81f;

    [Header("[Components]")] [SerializeField]
    private CharacterController _characterController;
    
    [Header("[Camera]")]
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _yawTarget;
    
    [Header("[Stats]")]
    [SerializeField] private float _speed;
    [SerializeField] private float _backwardSpeed = 3f;
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private bool _shouldFaceMoveDirection = false;
    
    private Vector3 _moveInput;
    private float _currentSpeed;

    private void Update() =>
        Move(Global.InputService.GetMoveInput());

    private void Move(Vector3 move)
    {
        _moveInput = move;
        
        Vector3 moveDirection = Vector3.zero;
        float targetSpeed = moveDirection.z < 0 ? _backwardSpeed : _speed;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, _acceleration * Time.deltaTime);

        moveDirection = MoveDirection(moveDirection);
        moveDirection.y -= Gravity * Time.deltaTime * _acceleration;
        
        _characterController.Move(_currentSpeed * Time.deltaTime * moveDirection);
    }

    private Vector3 MoveDirection(Vector3 moveDirection)
    {
        bool aimPressed = Global.InputService.AimPressed;
        
        if (aimPressed)
        {
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

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

        if (aimPressed)
        {
            Vector3 lookDirection = _yawTarget.forward;
            lookDirection.y = 0f;
        
            if (lookDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation =
                    Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _acceleration);
            }
        }
        else if (_shouldFaceMoveDirection && moveDirection.sqrMagnitude > 0.001f)
        {
            RotateTowardsMovement(moveDirection);
        }

        return moveDirection;
    }

    private void RotateTowardsMovement(Vector3 moveDirection)
    {
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation,
            _rotationSpeed * Time.deltaTime);
    }
}