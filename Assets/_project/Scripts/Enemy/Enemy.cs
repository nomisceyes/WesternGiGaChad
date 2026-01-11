using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IObject<Enemy>
{
    [SerializeField] private Player _player;

    [SerializeField] private Transform _popupPoint;
    [SerializeField] private EnemyMover _mover;

    public event Action<Enemy> Released;
    public event Action<Enemy> Died;

    public Health Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        Health.Died += Die;
    }

    private void OnDisable()
    {
        Health.Died -= Die;
    }

    private void Update()
    {
        if (_player.Health.IsAlive)
        {
            Move();
        }
    }

    public void Move()
    {
        _mover.MoveTo(_player.transform.position);
    }

    public void Warp(Vector3 position)
    {
        _mover.WarpTo(position);
    }

    public void TakeDamage(int damage)
    {
        Health.TakeDamage(_popupPoint, damage);
    }

    private void Die()
    {
        Released?.Invoke(this);
        Died?.Invoke(this);
    }

    public void SetPlayerTarget(Player player)
    {
        _player = player;
    }
}