using UnityEngine;

public class WeaponUser : MonoBehaviour
{
    [SerializeField] private RangeWeapon _gun;
    
    private void Update()
    {
        if (ServiceLocator.InputService.CanShoot())
        {
            _gun.Shooting();
        }
        else
        {
            _gun.Reloading();
        }
    }
}