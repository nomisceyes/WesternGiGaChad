using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private AudioSource _shootSound;
    [SerializeField] private AudioSource _reloadSound;

    [SerializeField] private CinemachineCamera _aimCamera;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private ParticleSystem _shootVFX;
    [SerializeField] private ParticleSystem _hitImpactVFX;
    [SerializeField] private Mover _mover;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private int _damage;
    [SerializeField] private int _maxAmountBullets = 5;

    [SerializeField] private int _currentAmoutBullets;
    private bool _isReloading = false;
    private bool _shootDelay = false;

    public event Action<int, int> AmmoChanged;

    private void Start()
    {
        _currentAmoutBullets = _maxAmountBullets;
    }

    private void Update()
    {
        if (_inputReader.IsShooting() && _mover.IsAiming && _currentAmoutBullets > 0 && _isReloading == false && _shootDelay == false)
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

        if (Physics.Raycast(_aimCamera.transform.position, _aimCamera.transform.forward, out RaycastHit hit, 300f, _layers))
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }

            HitEffect(hit.point, hit.normal);
            Debug.Log(hit.collider.name);
        }

        yield return new WaitForSeconds(0.3f);  // Исправить

        _shootDelay = false;
    }

    private void HitEffect(Vector3 position, Vector3 normal)
    {
        Vector3 offsetPosition = position + (normal * 0.05f);

        ParticleSystem hitEffect = Instantiate(_hitImpactVFX, offsetPosition, Quaternion.LookRotation(normal));

        _hitImpactVFX.Play();

        Destroy(hitEffect.gameObject, _hitImpactVFX.main.duration);
    }

    private IEnumerator Reload()
    {
        _isReloading = true;
        _reloadSound.PlayOneShot(_reloadSound.clip);

        yield return new WaitForSeconds(1.3f);  // Исправить

        _currentAmoutBullets = _maxAmountBullets;
        AmmoChanged?.Invoke(_currentAmoutBullets, _maxAmountBullets);

        _isReloading = false;
    }
}