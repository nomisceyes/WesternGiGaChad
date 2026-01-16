using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private Mover _mover;
    [SerializeField] private InputService _inputReader;

    private Animator _animator;

    public bool A = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();

        _animator.SetBool("Aiming", A);
    }

    private void Move()
    {
        _animator.SetFloat(Speed, _mover.CurrentSpeed);
    }
}