using DG.Tweening;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private readonly int _speed = Animator.StringToHash("Speed");
    private readonly int _horizontalSpeed = Animator.StringToHash("HorizontalSpeed");
    private readonly int _verticalSpeed = Animator.StringToHash("VerticalSpeed");
    private readonly int _aiming = Animator.StringToHash("Aiming");
    private readonly int _shooting = Animator.StringToHash("Shoot");
    private readonly int _isMelee = Animator.StringToHash("IsMelee");
    private readonly int _swordAttack = Animator.StringToHash("SwordAttack");

    private string Attack = "SwordAttack";

    [SerializeField] private float _smoothTime = 0.1f;

    private Animator _animator;
    private Mover _mover;
    private Vector2 _smoothVelocity;
    private float _currentHorizontalSpeed;
    private float _currentVerticalSpeed;
    private float _currentSpeed;
    
    private float _attackCooldown = 0f;
    private int _lastAttackIndex = -1;
    private float _resetTriggerDelay = 0.1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        if (Global.Main.PanelHandler.IsPaused == false)
        {
            Move();
            Aiming();
            Shooting();
            SwordAttack();

            EquipWeapon();
        }
    }

    private void Move()
    {
        SmoothInput(Global.InputService.GetMoveInput().x, Global.InputService.GetMoveInput().y);

        _animator.SetFloat(_horizontalSpeed, _currentHorizontalSpeed);
        _animator.SetFloat(_verticalSpeed, _currentVerticalSpeed);
        _animator.SetFloat(_speed, _currentSpeed);
    }

    private void SmoothInput(float x, float y)
    {
        Vector2 rawInput = new Vector2(x, y);
        Vector2 smoothInput = Vector2.SmoothDamp(new Vector2(_currentHorizontalSpeed, _currentVerticalSpeed), rawInput,
            ref _smoothVelocity, _smoothTime);

        _currentHorizontalSpeed = smoothInput.x;
        _currentVerticalSpeed = smoothInput.y;
        _currentSpeed = smoothInput.magnitude;
    }

    private void Aiming()
    {
        _animator.SetBool(_aiming, Global.InputService.IsAiming());
    }

    private void Shooting()
    {
        if (Global.InputService.IsShooting() && Global.Main.Player.WeaponUser.IsRange())
            _animator.SetTrigger(_shooting);
    }

    private void SwordAttack()
    {
        if (_attackCooldown > 0)
        {
            _attackCooldown -= Time.deltaTime;
            if (_attackCooldown <= 0 && _lastAttackIndex != -1)
            {
                _animator.ResetTrigger(Attack + _lastAttackIndex);
                _lastAttackIndex = -1;
            }
        }

        if (Global.InputService.IsShooting() && Global.Main.Player.WeaponUser.IsMelee())
        {
            if (Global.Main.Player.WeaponUser.TryToAttack())
            {
                int attackIndex = (int)Global.Main.Player.WeaponUser.CurrentIndexAttack;

                if (_lastAttackIndex != -1)
                {
                    _animator.ResetTrigger(Attack + _lastAttackIndex);
                }

                string triggerName = Attack + attackIndex;
                _animator.SetTrigger(triggerName);

                _lastAttackIndex = attackIndex;
                _attackCooldown = _resetTriggerDelay;
            }
        }
    }

    private void EquipWeapon()
    {
        switch (Global.Main.Player.WeaponUser.CurrentWeapon)
        {
            case RangeWeapon:
                _animator.SetBool(_isMelee, false);
                break;

            case MeleeWeapon:
                _animator.SetBool(_isMelee, true);
                break;
        }
    }
}