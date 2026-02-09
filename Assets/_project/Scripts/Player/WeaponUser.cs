using UnityEngine;

public class WeaponUser : MonoBehaviour
{
    [SerializeField] private RangeWeapon _gun;

    private IInputService _inputService;

    [Inject]
    public void Construct(IInputService inputService)
    {
        _inputService = inputService;
    }

    private void Update()
    {
        // if (_inputService.IsAiming())
        // {
        //     _gun.transform.SetPositionAndRotation(_aimGun.position, _aimGun.rotation);
        // }
        // else
        // {
        //     _gun.transform.SetPositionAndRotation(_idleGun.position, _idleGun.rotation);
        // }


        if (_inputService.IsShooting() && _inputService.AimPressed)
        {
            _gun.Shooting();
        }
        else
        {
            _gun.Reloading();
        }
    }
}