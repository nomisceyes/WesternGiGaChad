using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private readonly int _speed = Animator.StringToHash("Speed");

    [SerializeField] private Mover _mover;
    [SerializeField] private InputService _inputReader;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _animator.SetFloat(_speed, _mover.CurrentSpeed);
    }
}