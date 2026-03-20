using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private BossBullet _bullet;
    [SerializeField] private int _attackAmount;
    [SerializeField] private float _cooldown;
    
    private bool _attackIsFinish;

    private void Update()
    {
        if (_attackIsFinish == false)
            _cooldown -= Time.deltaTime;
        
        if (_player.IsAlive())
        {
            Move();
            Attack();
        }
    }

    private IEnumerator CreateBullet()
    {
        int _currentAttack = 0;

        while (_currentAttack != 3)
        {
            BossBullet bullet = Instantiate(_bullet, _player.transform.position, Quaternion.identity);
            bullet.Prepare();

            yield return new WaitForSeconds(1.5f);

            _currentAttack++;
        }

        _attackIsFinish = false;
    }

    private void Attack()
    {
        if (_cooldown < 0)
        {
            _attackIsFinish = true;
            _cooldown = 10f;
            StartCoroutine(CreateBullet());
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}