using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Mover : MonoBehaviour
{
    private const float Gravity = 9.81f;

    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _yawTarget;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _speed;
    [SerializeField] private float _backwardSpeed = 3f;
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private bool _shouldFaceMoveDirection = false;

    private Vector2 _moveInputer;
    private float _currentSpeed;

    public float CurrentSpeed => _moveInputer.sqrMagnitude;

    private void Start()
    {
        _moveInput.asset.Enable();
    }

    private void Update() =>
        Move();

    private void Move()
    {
        _moveInputer = _moveInput.action.ReadValue<Vector2>();
        Vector3 moveDirection = Vector3.zero;
        float targetSpeed = moveDirection.z < 0 ? _backwardSpeed : _speed;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, _acceleration * Time.deltaTime);

        moveDirection.y -= Gravity * Time.deltaTime;

        if (_inputReader.AimPressed)
        {
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            moveDirection = forward * _moveInputer.y + right * _moveInputer.x;
        }
        else
        {
            Vector3 forward = _camera.forward;
            Vector3 right = _camera.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            moveDirection = forward * _moveInputer.y + right * _moveInputer.x;
        }          
        
        _characterController.Move(_currentSpeed * Time.deltaTime * moveDirection);

        if (_inputReader.AimPressed)
        {
            Vector3 lookDirection = _yawTarget.forward;
            lookDirection.y = 0f;

            if (lookDirection.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
        else if(_shouldFaceMoveDirection && moveDirection.sqrMagnitude > 0.001f)
        {
            RotateTowardsMovement(moveDirection);
        }
    }

    private void RotateTowardsMovement(Vector3 moveDirection)
    {
        Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
    }
}