using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private readonly int _speed = Animator.StringToHash("Speed");
    private readonly int _aiming = Animator.StringToHash("Aiming");

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
    }

    private void Move()
    {
        _animator.SetFloat(_speed, _mover.CurrentSpeed);
    }

    private void Aiming()
    {
        // if(_on)
        // _animator.SetBool(_aiming, _on);
        
        _animator.SetBool(_aiming, _inputService.IsAiming());
    }
}