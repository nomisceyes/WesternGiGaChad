using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private CapsuleCollider _attackCollider;

    private bool _isAttacking = false;
    
    private void Awake()
    {
        _attackCollider = GetComponent<CapsuleCollider>();
        _attackCollider.isTrigger = true;
        _attackCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAttacking == false) return;
        
        if (other.TryGetComponent(out Enemy enemy))
        {
            int damage = Random.Range(MinDamage, MaxDamage + 1);
            enemy.TakeDamage(damage);
        }
    }

    public void Attack() =>
        StartCoroutine(AttackRoutine());

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true;
        _attackCollider.enabled = true;
        
        yield return new WaitForSeconds(0.5f);
        
        _isAttacking = false;
        _attackCollider.enabled = false;
    }
}