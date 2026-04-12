using UnityEngine;

public class WeaponUser : MonoBehaviour
{ 
    public RangeWeapon Gun;

    private void Awake()
    {
        Gun = GetComponentInChildren<RangeWeapon>();
    }
    
    private void Update()
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
}