using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour, IObject<Enemy>
{
    [SerializeField] protected Transform _popupPoint;
    [SerializeField] private EnemyMover _mover;

    public event Action<Enemy> Released;
    public event Action<Enemy> Died;

    private DeathRotate _deathRotate;
    
    bool can = false;
    
    public Health Health { get; private set; }

    private void Awake()
    {
        Health = GetComponent<Health>();
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

    protected void Move() =>
        _mover.MoveTo(Global.Main.Player.transform.position);

    public void SetStartPosition(Vector3 position) =>
        _mover.Warp(position);

    public void TakeDamage(int damage) =>
        Health.TakeDamage(_popupPoint, damage);

    private async UniTask TimeBeforeDie()
    {
        _mover._agent.enabled = false;
        _mover.enabled = false;
        
        _deathRotate.Trigger(gameObject);
        
        await UniTask.Delay(2000);
        
        can = true;
    }
    
    protected virtual void Die()
    {
        TimeBeforeDie();
        
        if (can)
        {
            Released?.Invoke(this);
            Died?.Invoke(this);
        }
    }
}