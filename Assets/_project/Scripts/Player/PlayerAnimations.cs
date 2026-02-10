using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const int BaseLayer = 0;
    private const int LegsLayer = 1;

    private readonly int _speed = Animator.StringToHash("Speed");
    private readonly int _horizontalSpeed = Animator.StringToHash("HorizontalSpeed");
    private readonly int _verticalSpeed = Animator.StringToHash("VerticalSpeed");
    private readonly int _aiming = Animator.StringToHash("Aiming");
    private readonly int _shooting = Animator.StringToHash("Shoot");

    [SerializeField] private bool _on;

    [SerializeField] private Mover _mover;
    [SerializeField] private float _smoothTime = 0.1f;
    private IInputService _inputService;

    private Animator _animator;
    
    private Vector2 _smoothVelocity;
    private float _currentHorizontalSpeed;
    private float _currentVerticalSpeed;
    private float _currentSpeed;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Aiming();
        Shooting();
    }

    private void Move()
    {
        SmoothInput();
        
        _animator.SetFloat(_horizontalSpeed, _currentHorizontalSpeed);
        _animator.SetFloat(_verticalSpeed, _currentVerticalSpeed);
        _animator.SetFloat(_speed, _currentSpeed);
    }

    private void SmoothInput()
    {
        Vector2 rawInput = new Vector2(_inputService.GetMoveInput().x, _inputService.GetMoveInput().y);
        Vector2 smoothInput = Vector2.SmoothDamp(new Vector2(_currentHorizontalSpeed, _currentVerticalSpeed), rawInput,
            ref _smoothVelocity, _smoothTime);
        
        _currentHorizontalSpeed = smoothInput.x;
        _currentVerticalSpeed = smoothInput.y;
        _currentSpeed = smoothInput.magnitude;
    }

    private void Aiming()
    {
        _animator.SetBool(_aiming, _inputService.IsAiming());
    }

    private void Shooting()
    {
        if (_inputService.IsShooting())
            _animator.SetTrigger(_shooting);
    }
}