using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IObject<Enemy>
{
    [SerializeField] private Transform _popupPoint;
    [SerializeField] private EnemyBulletSpawner _bulletSpawner;
    [SerializeField] private EnemyMover _mover;
    [SerializeField] private float _attackRange;

    private Player _player;

    public event Action<Enemy> Released;
    public event Action<Enemy> Died;

    public Health Health { get; private set; }

    private void Awake() =>
        Health = GetComponent<Health>();

    private void OnEnable() =>
        Health.Died += Die;

    private void OnDisable() =>
        Health.Died -= Die;

    private void Update()
    {
        if (_player.IsAlive())
        {
            Move();
        }

        if (_bulletSpawner != null && BaseCalculations.IsInRange(_player.transform.position, transform.position, _attackRange))
        {
            transform.LookAt(_player.transform);
            _bulletSpawner.Shoot();
        }
    }

    private void Move() =>
        _mover.MoveTo(_player.transform.position);

    public void SetStartPosition(Vector3 position) =>
        _mover.Warp(position);

    public void TakeDamage(int damage) =>
        Health.TakeDamage(_popupPoint, damage);

    public void SetPlayerTarget(Player player) =>
        _player = player;

    private void Die()
    {
        Released?.Invoke(this);
        Died?.Invoke(this);
    }
}