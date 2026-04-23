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
        
        if(IsMelee())
            _timer += Time.deltaTime;

        if (Global.InputService.SwitchWeapon())
        {
            SwitchWeapon();
        }
    }

    private void SwitchWeapon()
    {
        if(CurrentWeapon == IsRange())
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
        if (IsMelee())
            TryToAttack();
    }

    private void TryToAttack()
    {
        if (_timer >= _attackDelay)
        {
            CurrentIndexAttack++;

            if (CurrentIndexAttack > 2 || _timer > 3)
                CurrentIndexAttack = 1;
            
            _timer = 0f;
        }
    }

    public bool IsMelee() =>
        CurrentWeapon is MeleeWeapon;

    public bool IsRange() =>
        CurrentWeapon is RangeWeapon;
}