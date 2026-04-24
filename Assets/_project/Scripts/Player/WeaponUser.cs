using System.Collections;
using UnityEngine;

public class WeaponUser : MonoBehaviour
{
    public RangeWeapon Gun;
    public MeleeWeapon Sword;

    public Weapon CurrentWeapon { get; private set; }

    private float _attackDelay = 1f;
    public float CurrentIndexAttack;
    private float _timer;
    private bool _canAttack;

    private void Awake()
    {
        Gun = GetComponentInChildren<RangeWeapon>();
        Sword = GetComponentInChildren<MeleeWeapon>();
        Sword.gameObject.SetActive(false);

        CurrentWeapon = Gun;
    }

    private void Update()
    {
        if (IsRange())
        {
            if (Global.InputService.CanShoot())
            {
                Gun.Shooting();
            }
            else
            {
                Gun.Reloading();
            }
        }

        if (IsMelee())
        {
            _timer += Time.deltaTime;
            if (_timer >= _attackDelay)
                _canAttack = true;
        }

        if (Global.InputService.SwitchWeapon())
        {
            SwitchWeapon();
        }
    }

    private void SwitchWeapon()
    {
        if (CurrentWeapon == IsRange())
            EquipSword();
        else
            EquipGun();
    }

    private void EquipGun()
    {
        Sword.gameObject.SetActive(false);
        Gun.gameObject.SetActive(true);
        CurrentWeapon = Gun;
    }

    private void EquipSword()
    {
        Gun.gameObject.SetActive(false);
        Sword.gameObject.SetActive(true);
        CurrentWeapon = Sword;
    }

    public void Attack()
    {
        Debug.Log("Attack");

        if (IsMelee())
            TryToAttack();
    }

    public bool TryToAttack()
    {
        if (IsMelee() == false && _canAttack == false)
            return false;

        CurrentIndexAttack++;

        if (CurrentIndexAttack > 1)
            CurrentIndexAttack = 0;

        _timer = 0f;
        _canAttack = true;
        return true;
    }

    public bool IsMelee() =>
        CurrentWeapon is MeleeWeapon;

    public bool IsRange() =>
        CurrentWeapon is RangeWeapon;
}