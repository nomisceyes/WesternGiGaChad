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
    private IInputService _inputService;

    private Animator _animator;

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
        _animator.SetFloat(_horizontalSpeed, _inputService.GetMoveInput().x);
        _animator.SetFloat(_verticalSpeed, _inputService.GetMoveInput().y);
        
        float speed = new Vector2(_inputService.GetMoveInput().x, _inputService.GetMoveInput().y).magnitude;
        
        _animator.SetFloat(_speed, speed);
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