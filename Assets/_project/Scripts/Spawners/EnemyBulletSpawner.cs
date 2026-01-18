using System.Collections;
using UnityEngine;

public class EnemyBulletSpawner : Spawner<EnemyBullet>
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _timeOfReload = 2f;

    public WaitForSeconds TimeOfReload { get; private set; }

    private bool _isCharged = true;

    private void Start() =>
        TimeOfReload = new WaitForSeconds(_timeOfReload);

    public void Shoot()
    {
        if (_isCharged)
        {
            EnemyBullet bullet = Pool.Get();
            bullet.transform.SetPositionAndRotation(_firePoint.position, transform.rotation);
            bullet.SetDirection(transform.forward);

            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        _isCharged = false;

        yield return TimeOfReload;

        _isCharged = true;
    }

    protected override void ResetObject(EnemyBullet @object)
    {
        base.ResetObject(@object);
        @object.Reset();
    }
}