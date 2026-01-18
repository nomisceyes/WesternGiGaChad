using System;
using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IObject<EnemyBullet>
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private int _damage;
    [SerializeField] private float _lifetime = 1f;
    [SerializeField] private float _speed = 25f;

    public event Action<EnemyBullet> Released;

    private void OnEnable()
    {          
        StartCoroutine(DestroyAfterLifetime());
    }  

    private void OnCollisionEnter(Collision collision)
    {    
        if (collision.collider.TryGetComponent(out Player player) && player.IsAlive())
        {
            player.TakeDamage(_damage);
        Released?.Invoke(this);
        }

    }

    private IEnumerator DestroyAfterLifetime()
    {
        WaitForSeconds wait = new WaitForSeconds(_lifetime);

        yield return wait;

        Released?.Invoke(this);
    }

    public void SetDirection(Vector3 direction) =>
         _rigidbody.linearVelocity = direction * _speed;

    public void Reset()
    {
        _rigidbody.linearVelocity = Vector3.zero;
    }
}