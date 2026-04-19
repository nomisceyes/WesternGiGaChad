using System.Collections;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public CapsuleCollider AttackCollider;

    private void Awake()
    {
        AttackCollider = GetComponent<CapsuleCollider>();
        AttackCollider.isTrigger = true;
        AttackCollider.enabled = false;
    }

    
}