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
        if (ServiceLocator.InputService.CanShoot())
        {
            Gun.Shooting();
        }
        else
        {
            Gun.Reloading();
        }
    }
}