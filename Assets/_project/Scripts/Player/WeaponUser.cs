using System.Collections;
using UnityEngine;

public class WeaponUser : MonoBehaviour
{
    public RangeWeapon Gun;
    public MeleeWeapon Sword;

    public Weapon CurrentWeapon { get; private set; }

    private void Awake()
    {
        Gun = GetComponentInChildren<RangeWeapon>();
        Sword = GetComponentInChildren<MeleeWeapon>();
        Sword.gameObject.SetActive(false);
        
        CurrentWeapon = Gun;
    }

    private void Update()
    {
        if (Global.InputService.CanShoot() && Gun.gameObject.activeInHierarchy)
        {
            Gun.Shooting();
        }
        else
        {
            Gun.Reloading();
        }

        SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EquipSword();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EquipGun();
        }
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
    
    public void Attack() =>
        StartCoroutine(SwordAttack());
    
    private IEnumerator SwordAttack()
    {
        Debug.Log("Attack");
        Sword.AttackCollider.enabled = true;

        if (Sword.AttackCollider.TryGetComponent(out Enemy enemy))
        {
            int damage = Random.Range(Sword.MinDamage, Sword.MaxDamage + 1);
            enemy.TakeDamage(damage);
        }
        
        yield return new WaitForSeconds(0.5f);
        Sword.AttackCollider.enabled = false;
    }
}