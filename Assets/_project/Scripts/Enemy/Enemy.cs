using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour, IObject<Enemy>
{
    [SerializeField] protected Transform _popupPoint;
    [SerializeField] private Popup _popup;
    private EnemyMover _mover;

    public event Action<Enemy> Released;
    public event Action<Enemy> Died;

    private DeathRotate _deathRotate;

    public Health Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
        _mover = GetComponent<EnemyMover>();
        _deathRotate = GetComponent<DeathRotate>();
    }

    private void OnEnable() =>
        Health.Died += Die;

    private void OnDisable() =>
        Health.Died -= Die;

    private void Update()
    {
        if (Global.Main.Player.Health.IsAlive)
        {
            Move();
        }
    }

    protected void Move()
    {
        _mover.MoveTo(Global.Main.Player.transform.position);
    }

    public void SetStartPosition(Vector3 position) =>
        _mover.Warp(position);

    public void TakeDamage(int damage)
    {
        Health.TakeDamage(damage);
        _popup.Show(damage);
        
        Global.VFX.HitVFX.Play();
    }

    public void Reset()
    {
        _mover._agent.enabled = true;
        _mover.enabled = true;
        enabled = true;
    }

    private async UniTask TimeBeforeDie()
    {
        _mover._agent.enabled = false;
        _mover.enabled = false;
        enabled = false;
        _popup.Hide();

        await _deathRotate.TriggerAsync(gameObject);
        
        Died?.Invoke(this);
        Released?.Invoke(this);
    }

    protected virtual void Die()
    {
        _ = TimeBeforeDie();
    }
}