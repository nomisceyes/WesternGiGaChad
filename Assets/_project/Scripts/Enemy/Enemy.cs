using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IObject<Enemy>
{
    [SerializeField] protected Transform _popupPoint;
    [SerializeField] private EnemyMover _mover;

    protected Player _player;

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
    }

    protected void Move() =>
        _mover.MoveTo(_player.transform.position);

    public void SetStartPosition(Vector3 position) =>
        _mover.Warp(position);

    public void TakeDamage(int damage) =>
        Health.TakeDamage(_popupPoint, damage);

    public void SetPlayerTarget(Player player) =>
        _player = player;

    protected virtual void Die()
    {
        Released?.Invoke(this);
        Died?.Invoke(this);
    }
}