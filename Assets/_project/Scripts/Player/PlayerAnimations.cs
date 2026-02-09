using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const int BaseLayer = 0;
    private const int LegsLayer = 1;
    
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
        // if (_inputService.IsAiming() && _mover.CurrentSpeed > 0.1f)
        // {
        //     _animator.SetLayerWeight(BaseLayer, 0);
        //     _animator.SetLayerWeight(LegsLayer, 0.5f);
        // }
        // else
        // {
        //     _animator.SetLayerWeight(LegsLayer, 0);
        //     _animator.SetLayerWeight(BaseLayer, 1);
        // }
        
        Move();
        Aiming();
    }

    private void Move()
    {
        _animator.SetFloat(_speed, _mover.CurrentSpeed);
    }
    
    private void Aiming()
    {
        _animator.SetBool(_aiming, _inputService.IsAiming());
    }
}