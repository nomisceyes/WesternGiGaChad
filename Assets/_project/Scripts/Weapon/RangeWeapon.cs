using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeapon : Weapon
{
    private const int MinAmountAmmo = 0;

    [SerializeField] private Transform _shootPosition;
    [SerializeField] private CinemachineCamera _aimCamera;
    [SerializeField] private int _maxShootDistance = 300;
    [SerializeField] private float _timeBetweenShoot = 0.3f;
    [SerializeField] private float _reloadTime = 0.3f;

    public int MaxAmmo = 5;
    private WaitForSeconds _shootDelayTime;
    private WaitForSeconds _reloadDelayTime;

    public int CurrentAmmo;
    private bool _isReloading = false;
    private bool _shootDelay = false;

    public event Action<int, int> AmmoChanged;

    private void Start()
    {
        CurrentAmmo = MaxAmmo;

        _shootDelayTime = new WaitForSeconds(_timeBetweenShoot);
        _reloadDelayTime = new WaitForSeconds(_reloadTime);
    }

    private void OnDisable() =>
        StopAllCoroutines();

    public void Shooting()
    {
        if (CurrentAmmo > MinAmountAmmo && _isReloading == false && _shootDelay == false)
        {
            _shootDelay = true;
            StartCoroutine(Shoot());
        }
    }

    public void Reloading()
    {
        if (CurrentAmmo == 0 && _isReloading == false)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Shoot()
    {
        Global.AudioManager.PlaySound(Res.Audio.RifleShootSound, 0.5f);
        Global.VFX.RifleShootVFX.transform.position = _shootPosition.position;
        Global.VFX.RifleShootVFX.Play();

        CurrentAmmo--;
        AmmoChanged?.Invoke(CurrentAmmo, MaxAmmo);

        if (Physics.Raycast(_aimCamera.transform.position, _aimCamera.transform.forward, out RaycastHit hit,
                _maxShootDistance, Layers))
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                int damage = Random.Range(MinDamage, MaxDamage + 1);

                enemy.TakeDamage(damage);
            }
        }

        yield return _shootDelayTime;

        _shootDelay = false;
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        Global.AudioManager.PlaySound(Res.Audio.RifleReloadSound);

        yield return _reloadDelayTime;

        CurrentAmmo = MaxAmmo;
        AmmoChanged?.Invoke(CurrentAmmo, MaxAmmo);

        _isReloading = false;
    }
}