using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeapon : Weapon
{
    private const int MinAmountAmmo = 0;

    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioSource _reloadSound;
    [SerializeField] private ParticleSystem _shootVFX;
    [SerializeField] private ParticleSystem _hitImpactVFX;

    [SerializeField] private CinemachineCamera _aimCamera;

    [SerializeField] private int _maxAmountBullets = 5;
    [SerializeField] private int _maxShootDistance = 300;
    [SerializeField] private float _timeBetweenShoot = 0.3f;
    [SerializeField] private float _reloadTime = 0.3f;

    private WaitForSeconds _shootDelayTime;
    private WaitForSeconds _reloadDelayTime;

    private int _currentAmoutBullets;
    private bool _isReloading = false;
    private bool _shootDelay = false;

    public event Action<int, int> AmmoChanged;

    private void Start()
    {
        _currentAmoutBullets = _maxAmountBullets;

        _shootDelayTime = new WaitForSeconds(_timeBetweenShoot);
        _reloadDelayTime = new WaitForSeconds(_reloadTime);
    }

    private void Update()
    {
        if (inputService.IsShooting() && inputService.AimPressed && _currentAmoutBullets > MinAmountAmmo && _isReloading == false && _shootDelay == false)
        {
            StartCoroutine(Shoot());
        }

        if (_currentAmoutBullets == 0 && _isReloading == false)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Shoot()
    {
        _shootDelay = true;

        _shootSound.Play();
        _shootVFX.Play();

        _currentAmoutBullets--;
        AmmoChanged?.Invoke(_currentAmoutBullets, _maxAmountBullets);

        if (Physics.Raycast(_aimCamera.transform.position, _aimCamera.transform.forward, out RaycastHit hit, _maxShootDistance, Layers))
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                int damage = Random.Range(minDamage, maxDamage + 1);

                enemy.TakeDamage(damage);
            }

            HitEffect(_hitImpactVFX, hit.point, hit.normal);    
        }

        yield return _shootDelayTime;

        _shootDelay = false;
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        _reloadSound.PlayOneShot(_reloadSound.clip);

        yield return _reloadDelayTime;

        _currentAmoutBullets = _maxAmountBullets;
        AmmoChanged?.Invoke(_currentAmoutBullets, _maxAmountBullets);

        _isReloading = false;
    }
}