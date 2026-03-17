using System.Collections;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private BossBullet _bullet;
    [SerializeField] private float _cooldown;

    private int _attackAmount = 3;

    private bool _attackIsFinish = false;

    private void Update()
    {
        if (_attackIsFinish == false)
            _cooldown -= Time.deltaTime;
        
        Attack();
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
        
        _attackIsFinish =  false;
    }
}